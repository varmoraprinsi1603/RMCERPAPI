
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMCERPAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin,Support Executive,User")]
    public class TicketAttachmentController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        private static readonly string[] AllowedExtensions =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp",
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt"
        };

        private const long MaxFileSize = 10 * 1024 * 1024;
        private const long MaxRequestSize = MaxFileSize + 1024 * 1024;
        private const long MaxTextFileSize = 1024 * 1024;

        private const string MetadataSuffix = ".meta.json";

        private sealed class AttachmentMetadata
        {
            public string OriginalFileName { get; set; } = string.Empty;
        }

        public TicketAttachmentController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [RequestSizeLimit(MaxRequestSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxRequestSize)]
        public async Task<IActionResult> Upload(
            [FromForm] decimal TicketID,
            [FromForm] IFormFile file)
        {
            string filePath = null;
            string metadataPath = null;

            try
            {
                if (TicketID <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid TicketID."
                    });
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Please select a file."
                    });
                }

                if (file.Length > MaxFileSize)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "File size cannot exceed 10 MB."
                    });
                }

                var originalName = Path.GetFileName(file.FileName);

                if (string.IsNullOrWhiteSpace(originalName))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid file name."
                    });
                }

                var extension = Path.GetExtension(originalName)
                    .ToLowerInvariant();

                if (!AllowedExtensions.Contains(extension))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "This file type is not allowed."
                    });
                }

                if (!IsValidFileSignature(file, extension))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid file content for the selected file type."
                    });
                }

                var folder = GetTicketFolder(TicketID);
                Directory.CreateDirectory(folder);

                // Original filename is not used as the physical storage filename.
                var storedName = $"{Guid.NewGuid():N}{extension}";

                filePath = Path.Combine(folder, storedName);
                metadataPath = GetMetadataPath(folder, storedName);

                await using (var stream = new FileStream(
                    filePath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None))
                {
                    await file.CopyToAsync(stream);
                }

                var metadata = new AttachmentMetadata
                {
                    OriginalFileName = originalName
                };

                var metadataJson = JsonSerializer.Serialize(metadata);

                await System.IO.File.WriteAllTextAsync(
                    metadataPath,
                    metadataJson,
                    Encoding.UTF8);

                return Ok(new
                {
                    Status = 1,
                    Message = "File uploaded successfully.",
                    Data = new
                    {
                        FileName = originalName,
                        StoredFileName = storedName,
                        Size = file.Length,
                        Extension = extension,
                        IsImage = IsImage(extension)
                    }
                });
            }
            catch (Exception)
            {
                // Clean up partial files if upload or metadata saving fails.
                TryDeleteFile(filePath);
                TryDeleteFile(metadataPath);

                return StatusCode(500, new
                {
                    Status = 0,
                    Message = "An error occurred while uploading the attachment."
                });
            }
        }

        [HttpGet]
        public IActionResult GetFiles(decimal TicketID)
        {
            try
            {
                if (TicketID <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid TicketID."
                    });
                }

                var folder = GetTicketFolder(TicketID);

                if (!Directory.Exists(folder))
                {
                    return Ok(new
                    {
                        Status = 1,
                        Data = Array.Empty<object>(),
                        Message = "No attachments found."
                    });
                }

                var files = Directory.GetFiles(folder)
                    .Where(path =>
                        !path.EndsWith(
                            MetadataSuffix,
                            StringComparison.OrdinalIgnoreCase))
                    .Where(path =>
                        AllowedExtensions.Contains(
                            Path.GetExtension(path).ToLowerInvariant()))
                    .Select(path =>
                    {
                        var info = new FileInfo(path);
                        var extension = info.Extension.ToLowerInvariant();

                        return new
                        {
                            FileName = GetOriginalFileName(folder, info.Name),
                            StoredFileName = info.Name,
                            Size = info.Length,
                            Extension = extension,
                            IsImage = IsImage(extension)
                        };
                    })
                    .OrderByDescending(x => x.StoredFileName)
                    .ToList();

                return Ok(new
                {
                    Status = 1,
                    Data = files,
                    Message = "Attachments fetched successfully."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Status = 0,
                    Message = "An error occurred while fetching attachments."
                });
            }
        }

        [HttpGet]
        public IActionResult Download(decimal TicketID, string fileName)
        {
            try
            {
                if (TicketID <= 0 || string.IsNullOrWhiteSpace(fileName))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid attachment details."
                    });
                }

                var safeName = Path.GetFileName(fileName);

                if (safeName.EndsWith(
                    MetadataSuffix,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid attachment name."
                    });
                }

                var extension = Path.GetExtension(safeName)
                    .ToLowerInvariant();

                if (!AllowedExtensions.Contains(extension))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid attachment type."
                    });
                }

                var folder = GetTicketFolder(TicketID);
                var filePath = Path.Combine(folder, safeName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "File not found."
                    });
                }

                var contentType = GetContentType(extension);
                var downloadName = GetOriginalFileName(folder, safeName);

                return PhysicalFile(
                    filePath,
                    contentType,
                    downloadName,
                    enableRangeProcessing: true);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Status = 0,
                    Message = "An error occurred while opening the attachment."
                });
            }
        }

        [HttpDelete]
        public IActionResult Delete(decimal TicketID, string fileName)
        {
            try
            {
                if (TicketID <= 0 || string.IsNullOrWhiteSpace(fileName))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid attachment details."
                    });
                }

                var safeName = Path.GetFileName(fileName);

                if (safeName.EndsWith(
                    MetadataSuffix,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid attachment name."
                    });
                }

                var extension = Path.GetExtension(safeName)
                    .ToLowerInvariant();

                if (!AllowedExtensions.Contains(extension))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid attachment type."
                    });
                }

                var folder = GetTicketFolder(TicketID);
                var filePath = Path.Combine(folder, safeName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "File not found."
                    });
                }

                // Delete the attachment and its metadata.
                System.IO.File.Delete(filePath);

                var metadataPath = GetMetadataPath(folder, safeName);
                TryDeleteFile(metadataPath);

                return Ok(new
                {
                    Status = 1,
                    Message = "Attachment deleted successfully."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Status = 0,
                    Message = "An error occurred while deleting the attachment."
                });
            }
        }

        private string GetTicketFolder(decimal ticketID)
        {
            return Path.Combine(
                _environment.ContentRootPath,
                "Uploads",
                "Tickets",
                ticketID.ToString("0")
            );
        }

        private static string GetMetadataPath(
            string folder,
            string storedName)
        {
            return Path.Combine(folder, storedName + MetadataSuffix);
        }

        private static string GetOriginalFileName(
            string folder,
            string storedName)
        {
            var metadataPath = GetMetadataPath(folder, storedName);

            if (System.IO.File.Exists(metadataPath))
            {
                try
                {
                    var json = System.IO.File.ReadAllText(metadataPath);

                    var metadata =
                        JsonSerializer.Deserialize<AttachmentMetadata>(json);

                    if (!string.IsNullOrWhiteSpace(metadata?.OriginalFileName))
                    {
                        return Path.GetFileName(metadata.OriginalFileName);
                    }
                }
                catch (JsonException)
                {
                    // Fall back to the stored name if metadata is invalid.
                }
                catch (IOException)
                {
                    // Fall back to the stored name if metadata cannot be read.
                }
            }

            // Fallback for older attachments without metadata.
            return storedName;
        }

        private static void TryDeleteFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (IOException)
            {
                // Do not expose server file-system details to the client.
            }
            catch (UnauthorizedAccessException)
            {
                // Cleanup can be retried after file permissions are checked.
            }
        }

        private static bool IsValidFileSignature(
            IFormFile file,
            string extension)
        {
            using var stream = file.OpenReadStream();

            byte[] header = new byte[8];
            int bytesRead = stream.Read(header, 0, header.Length);

            switch (extension.ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg":
                    return bytesRead >= 3 &&
                           header[0] == 0xFF &&
                           header[1] == 0xD8 &&
                           header[2] == 0xFF;

                case ".png":
                    return bytesRead >= 8 &&
                           header[0] == 0x89 &&
                           header[1] == 0x50 &&
                           header[2] == 0x4E &&
                           header[3] == 0x47 &&
                           header[4] == 0x0D &&
                           header[5] == 0x0A &&
                           header[6] == 0x1A &&
                           header[7] == 0x0A;

                case ".gif":
                    return bytesRead >= 6 &&
                           (Encoding.ASCII.GetString(header, 0, 6) == "GIF87a" ||
                            Encoding.ASCII.GetString(header, 0, 6) == "GIF89a");

                case ".webp":
                    if (bytesRead < 8)
                        return false;

                    byte[] webpHeader = new byte[12];
                    stream.Position = 0;

                    if (stream.Read(webpHeader, 0, 12) < 12)
                        return false;

                    return Encoding.ASCII.GetString(webpHeader, 0, 4) == "RIFF" &&
                           Encoding.ASCII.GetString(webpHeader, 8, 4) == "WEBP";

                case ".pdf":
                    return bytesRead >= 5 &&
                           Encoding.ASCII.GetString(header, 0, 5) == "%PDF-";

                case ".doc":
                case ".xls":
                    return IsOleDocument(stream);

                case ".docx":
                    return IsOfficeZipFile(stream, "word/document.xml");

                case ".xlsx":
                    return IsOfficeZipFile(stream, "xl/workbook.xml");

                case ".txt":
                    return IsTextFile(stream);

                default:
                    return false;
            }
        }

        private static bool IsOleDocument(Stream stream)
        {
            byte[] signature =
            {
                0xD0, 0xCF, 0x11, 0xE0,
                0xA1, 0xB1, 0x1A, 0xE1
            };

            byte[] buffer = new byte[signature.Length];

            stream.Position = 0;

            return stream.Read(buffer, 0, buffer.Length) == buffer.Length &&
                   buffer.SequenceEqual(signature);
        }

        private static bool IsOfficeZipFile(
            Stream stream,
            string requiredEntry)
        {
            try
            {
                stream.Position = 0;

                using var archive = new ZipArchive(
                    stream,
                    ZipArchiveMode.Read,
                    leaveOpen: true);

                bool hasContentTypes = archive.Entries.Any(
                    entry => entry.FullName.Equals(
                        "[Content_Types].xml",
                        StringComparison.OrdinalIgnoreCase));

                bool hasRequiredEntry = archive.Entries.Any(
                    entry => entry.FullName.Equals(
                        requiredEntry,
                        StringComparison.OrdinalIgnoreCase));

                return hasContentTypes && hasRequiredEntry;
            }
            catch (InvalidDataException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                stream.Position = 0;
            }
        }

        private static bool IsTextFile(Stream stream)
        {
            if (stream.Length > MaxTextFileSize)
                return false;

            stream.Position = 0;

            try
            {
                // Detects UTF-8, UTF-16 and UTF-32 BOMs.
                // Without a BOM, strict UTF-8 decoding is used.
                using var reader = new StreamReader(
                    stream,
                    new UTF8Encoding(false, true),
                    detectEncodingFromByteOrderMarks: true,
                    bufferSize: 1024,
                    leaveOpen: true);

                int value;

                while ((value = reader.Read()) != -1)
                {
                    char character = (char)value;

                    if (char.IsControl(character) &&
                        character != '\r' &&
                        character != '\n' &&
                        character != '\t' &&
                        character != '\f')
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (DecoderFallbackException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                stream.Position = 0;
            }
        }

        private static bool IsImage(string extension)
        {
            return extension == ".jpg" ||
                   extension == ".jpeg" ||
                   extension == ".png" ||
                   extension == ".gif" ||
                   extension == ".webp";
        }

        private static string GetContentType(string extension)
        {
            return extension.ToLowerInvariant() switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }
    }
}
