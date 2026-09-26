using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public TicketAttachmentController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [RequestSizeLimit(MaxFileSize)]
        public async Task<IActionResult> Upload([FromForm] decimal TicketID, [FromForm] IFormFile file)
        {
            try
            {
                if (TicketID <= 0)
                    return BadRequest(new { Status = 0, Message = "Invalid TicketID." });

                if (file == null || file.Length == 0)
                    return BadRequest(new { Status = 0, Message = "Please select a file." });

                if (file.Length > MaxFileSize)
                    return BadRequest(new { Status = 0, Message = "File size cannot exceed 10 MB." });

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!AllowedExtensions.Contains(extension))
                    return BadRequest(new { Status = 0, Message = "This file type is not allowed." });

                var folder = GetTicketFolder(TicketID);
                Directory.CreateDirectory(folder);

                var safeName = Path.GetFileName(file.FileName);
                var uniqueName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}_{safeName}";
                var filePath = Path.Combine(folder, uniqueName);

                await using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new
                {
                    Status = 1,
                    Message = "File uploaded successfully.",
                    Data = new
                    {
                        FileName = safeName,
                        StoredFileName = uniqueName,
                        Size = file.Length,
                        Extension = extension,
                        IsImage = IsImage(extension)
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error uploading file: {ex.Message}"
                });
            }
        }

        [HttpGet]
        public IActionResult GetFiles(decimal TicketID)
        {
            try
            {
                if (TicketID <= 0)
                    return BadRequest(new { Status = 0, Message = "Invalid TicketID." });

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
                    .Select(path =>
                    {
                        var info = new FileInfo(path);
                        var extension = info.Extension.ToLowerInvariant();

                        return new
                        {
                            FileName = GetOriginalFileName(info.Name),
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching attachments: {ex.Message}"
                });
            }
        }

        [HttpGet]
        public IActionResult Download(decimal TicketID, string fileName)
        {
            try
            {
                if (TicketID <= 0 || string.IsNullOrWhiteSpace(fileName))
                    return BadRequest(new { Status = 0, Message = "Invalid attachment details." });

                var safeName = Path.GetFileName(fileName);
                var folder = GetTicketFolder(TicketID);
                var filePath = Path.Combine(folder, safeName);

                if (!System.IO.File.Exists(filePath))
                    return NotFound(new { Status = 0, Message = "File not found." });

                var contentType = GetContentType(Path.GetExtension(filePath));
                return PhysicalFile(filePath, contentType, GetOriginalFileName(safeName), enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error opening attachment: {ex.Message}"
                });
            }
        }

        [HttpDelete]
        public IActionResult Delete(decimal TicketID, string fileName)
        {
            try
            {
                if (TicketID <= 0 || string.IsNullOrWhiteSpace(fileName))
                    return BadRequest(new { Status = 0, Message = "Invalid attachment details." });

                var safeName = Path.GetFileName(fileName);
                var folder = GetTicketFolder(TicketID);
                var filePath = Path.Combine(folder, safeName);

                if (!System.IO.File.Exists(filePath))
                    return NotFound(new { Status = 0, Message = "File not found." });

                System.IO.File.Delete(filePath);

                return Ok(new
                {
                    Status = 1,
                    Message = "Attachment deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error deleting attachment: {ex.Message}"
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

        private static string GetOriginalFileName(string storedName)
        {
            var firstUnderscore = storedName.IndexOf('_');
            var secondUnderscore = firstUnderscore >= 0
                ? storedName.IndexOf('_', firstUnderscore + 1)
                : -1;

            if (secondUnderscore >= 0 && secondUnderscore + 1 < storedName.Length)
                return storedName.Substring(secondUnderscore + 1);

            return storedName;
        }
    }
}
