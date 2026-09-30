using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMCERPAPI.Models;
using RMCERPAPI.Repository;
using System;
using System.IO;

namespace RMCERPAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly TicketRepository _ticketRepository;

        public TicketController(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }


        // =========================================
        // 1. Get Ticket List
        // =========================================
        [HttpGet]
        public IActionResult GetTicketList()
        {
            try
            {
                var data = _ticketRepository.GetTicketList();

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket list fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching ticket list: {ex.Message}"
                });
            }
        }


        // =========================================
        // 2. Get Ticket By ID
        // =========================================
        [HttpGet]
        public IActionResult GetTicketByID(decimal TicketID)
        {
            try
            {
                var data = _ticketRepository.GetTicketByID(TicketID);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Ticket not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching ticket: {ex.Message}"
                });
            }
        }


        // =========================================
        // 3. Create Ticket
        // =========================================
        [HttpPost]
        [Authorize(Roles = "User,Admin,Support Executive")]
        public IActionResult AddTicket([FromBody] TicketModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid ticket data."
                    });
                }

                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Title is required."
                    });
                }

                if (model.CategoryID <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Category is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(model.Priority))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Priority is required."
                    });
                }

                decimal ticketID =
                    _ticketRepository.AddTicket(model);

                return Ok(new
                {
                    TicketID = ticketID,
                    Status = 1,
                    Message = "Ticket created successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error creating ticket: {ex.Message}"
                });
            }
        }


        // =========================================
        // 4. Assign Ticket
        // =========================================
        [HttpPut]
        [Authorize(Roles = "Admin,Support Executive")]
        public IActionResult AssignTicket(
        decimal TicketID,
        decimal AssignedTo,
        decimal AssignBy)
        {
            try
            {
                if (TicketID <= 0 ||
                    AssignedTo <= 0 ||
                    AssignBy <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid TicketID, AssignedTo or AssignBy."
                    });
                }

                var data =
                    _ticketRepository.AssignTicket(
                        TicketID,
                        AssignedTo,
                        AssignBy
                    );

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Ticket not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket assigned successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error assigning ticket: {ex.Message}"
                });
            }
        }

        // =========================================
        // 5. Change Ticket Status
        // =========================================
        [HttpPut]
        [Authorize(Roles = "Admin,Support Executive,User")]
        public IActionResult ChangeStatus(
            decimal TicketID,
            string Status,
            string OldStatus,
            decimal ChangedBy)
        {
            try
            {
                if (TicketID <= 0 || ChangedBy <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid TicketID or ChangedBy."
                    });
                }

                if (string.IsNullOrWhiteSpace(Status))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Status is required."
                    });
                }

                var data =
                    _ticketRepository.ChangeStatus(
                        TicketID,
                        Status,
                        OldStatus,
                        ChangedBy
                    );

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Ticket not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket status updated successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error updating ticket status: {ex.Message}"
                });
            }
        }


        // =========================================
        // 6. Get Ticket History
        // =========================================
        [HttpGet]
        public IActionResult GetTicketHistory(decimal TicketID)
        {
            try
            {
                var data =
                    _ticketRepository.GetTicketHistory(TicketID);

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket history fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching ticket history: {ex.Message}"
                });
            }
        }


        // =========================================
        // 7. Get Categories
        // =========================================
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                var data = _ticketRepository.GetCategories();

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket categories fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching categories: {ex.Message}"
                });
            }
        }


        // =========================================
        // 8. Get Support Executives
        // =========================================
        [HttpGet]
        [Authorize(Roles = "Admin,Support Executive,User")]
        public IActionResult GetSupportExecutives()
        {
            try
            {
                var data =
                    _ticketRepository.GetSupportExecutives();

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Support executives fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching support executives: {ex.Message}"
                });
            }
        }

        // =========================================
        // UpdateTicket
        // =========================================
        [HttpPut]
        [Authorize(Roles = "Admin,Support Executive")]
        public IActionResult UpdateTicket([FromBody] TicketModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid ticket data."
                    });
                }

                if (model.TicketID <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "TicketID is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Title is required."
                    });
                }

                if (model.CategoryID <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Category is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(model.Priority))
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Priority is required."
                    });
                }

                var data = _ticketRepository.UpdateTicket(model);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Ticket not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket updated successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error updating ticket: {ex.Message}"
                });
            }
        }

        // =========================================
        // DeleteTicket
        // =========================================
        [HttpDelete]
        [Authorize(Roles = "Admin,Support Executive")]
        public IActionResult DeleteTicket(decimal TicketID)
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

                decimal deletedTicketID =
                    _ticketRepository.DeleteTicket(TicketID);

                if (deletedTicketID <= 0)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Ticket not found."
                    });
                }

                return Ok(new
                {
                    TicketID = deletedTicketID,
                    Status = 1,
                    Message = "Ticket deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error deleting ticket: {ex.Message}"
                });
            }
        }

        // =========================================================
        // GENERATE TICKET SUMMARY REPORT
        // =========================================================
        //[Authorize(Roles = "Admin, Support Executive, User")]
        //[HttpGet]
        //public IActionResult GenerateTicketSummaryReport(
        //    string FromDate = null,
        //    string ToDate = null,
        //    string Status = null,
        //    string Priority = null,
        //    string AssignedToName = null)
        //{
        //    try
        //    {
        //        string reportUrl =
        //            "https://localhost:44390/CrystalReports/ReportViewer.aspx" +
        //            "?ReportName=TicketSummary" +
        //            "&TableName=TicketSummary" +
        //            "&FromDate=" + Uri.EscapeDataString(FromDate ?? "") +
        //            "&ToDate=" + Uri.EscapeDataString(ToDate ?? "") +
        //            "&Status=" + Uri.EscapeDataString(Status ?? "") +
        //            "&Priority=" + Uri.EscapeDataString(Priority ?? "") +
        //            "&AssignedToName=" + Uri.EscapeDataString(AssignedToName ?? "");

        //        return Ok(new
        //        {
        //            Data = new
        //            {
        //                ReportUrl = reportUrl
        //            },
        //            Status = 1,
        //            Message = "Ticket Summary report URL generated successfully."
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            Status = 0,
        //            Message = $"Error generating Ticket Summary report: {ex.Message}"
        //        });
        //    }
        //}

        // =========================================================
        // EXPORT TICKET SUMMARY TO EXCEL
        // =========================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Support Executive,User")]
        public IActionResult ExportTicketSummaryExcel(
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    string Status = null,
    string Priority = null,
    string AssignedToName = null)
        {
            try
            {
                var data = _ticketRepository.GetTicketSummaryReport(
                    FromDate,
                    ToDate,
                    Status,
                    Priority,
                    AssignedToName
                );

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Ticket Summary");

                    // =========================
                    // Headers
                    // =========================
                    worksheet.Cell(1, 1).Value = "Ticket No";
                    worksheet.Cell(1, 2).Value = "Title";
                    worksheet.Cell(1, 3).Value = "Category";
                    worksheet.Cell(1, 4).Value = "Priority";
                    worksheet.Cell(1, 5).Value = "Status";
                    worksheet.Cell(1, 6).Value = "AssignedToName";
                    worksheet.Cell(1, 7).Value = "Created Date";

                    // =========================
                    // Header Styling
                    // =========================
                    var headerRange = worksheet.Range(1, 1, 1, 7);

                    headerRange.Style.Font.Bold = true;

                    headerRange.Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    headerRange.Style.Alignment.Vertical =
                        ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                    int row = 2;

                    // =========================
                    // Data
                    // =========================
                    foreach (var item in data)
                    {
                        worksheet.Cell(row, 1).Value =
                            item.TicketNo ?? "";

                        worksheet.Cell(row, 2).Value =
                            item.Title ?? "";

                        worksheet.Cell(row, 3).Value =
                            item.Category ?? "";

                        worksheet.Cell(row, 4).Value =
                            item.Priority ?? "";

                        worksheet.Cell(row, 5).Value =
                            item.Status ?? "";

                        worksheet.Cell(row, 6).Value =
                            item.AssignedToName ?? "";

                        // Created Date
                        // Write as formatted text so Excel does not
                        // automatically right-align or show ####
                        worksheet.Cell(row, 7).Value =
                            item.CreatedDate.ToString("dd-MM-yyyy HH:mm:ss");

                        // Exact center alignment for Created Date
                        worksheet.Cell(row, 7).Style.Alignment.Horizontal =
                            ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                        worksheet.Cell(row, 7).Style.Alignment.Vertical =
                            ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                        row++;
                    }

                    // =========================
                    // Column Widths
                    // =========================
                    worksheet.Column(1).Width = 15;  // Ticket No

                    worksheet.Column(2).Width = 40;  // Title
                    worksheet.Column(2).Style.Alignment.WrapText = true;

                    worksheet.Column(3).Width = 18;  // Category
                    worksheet.Column(4).Width = 15;  // Priority
                    worksheet.Column(5).Width = 15;  // Status
                    worksheet.Column(6).Width = 22;  // Assigned To

                    // Created Date
                    worksheet.Column(7).Width = 24;

                    // =========================
                    // Alignment
                    // =========================

                    // Created Date column - center
                    worksheet.Range(1, 7, row - 1, 7)
                        .Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    worksheet.Range(1, 7, row - 1, 7)
                        .Style.Alignment.Vertical =
                        ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                    // Title column - vertical center + wrap
                    worksheet.Column(2).Style.Alignment.Vertical =
                        ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                    worksheet.Column(2).Style.Alignment.WrapText = true;

                    // =========================
                    // Row Height
                    // =========================
                    worksheet.Rows().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "TicketSummary.xlsx"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error exporting Ticket Summary: {ex.Message}"
                });
            }
        }
        // =========================================================
        // SEARCH TICKET SUMMARY REPORT
        // =========================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Support Executive,User")]
        public IActionResult GetTicketSummaryReport(
            DateTime? FromDate = null,
            DateTime? ToDate = null,
            string Status = null,
            string Priority = null,
            string AssignedToName = null)
        {
            try
            {
                var data = _ticketRepository.GetTicketSummaryReport(
                    FromDate,
                    ToDate,
                    Status,
                    Priority,
                    AssignedToName
                );

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Ticket Summary fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching Ticket Summary: {ex.Message}"
                });
            }
        }


        // =========================================================
        // GetTicketPerformanceReport
        // =========================================================

        [HttpGet]
        [Authorize(Roles = "Admin,Support Executive,User")]
        public IActionResult GetTicketPerformanceReport(
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    string Status = null,
    string Priority = null,
    string AssignedToName = null)
        {
            try
            {
                var data = _ticketRepository.GetTicketPerformanceReport(
                    FromDate,
                    ToDate,
                    Status,
                    Priority,
                    AssignedToName
                );

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error getting Ticket Performance Report: {ex.Message}"
                });
            }
        }

        // =========================================================
        // EXPORT TICKET PERFORMANCE TO EXCEL
        // =========================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Support Executive,User")]
        public IActionResult ExportTicketPerformanceExcel(
            DateTime? FromDate = null,
            DateTime? ToDate = null,
            string Status = null,
            string Priority = null,
            string AssignedToName = null)
        {
            try
            {
                var data = _ticketRepository.GetTicketPerformanceReport(
                    FromDate,
                    ToDate,
                    Status,
                    Priority,
                    AssignedToName
                );

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet =
                        workbook.Worksheets.Add("Ticket Performance");

                    // =========================
                    // Headers
                    // =========================
                    worksheet.Cell(1, 1).Value =
                        "Support Executive";

                    worksheet.Cell(1, 2).Value =
                        "Total Assigned";

                    worksheet.Cell(1, 3).Value =
                        "Resolved";

                    worksheet.Cell(1, 4).Value =
                        "Pending";

                    worksheet.Cell(1, 5).Value =
                        "Average Resolution Time";

                    // =========================
                    // Header Styling
                    // =========================
                    var headerRange =
                        worksheet.Range(1, 1, 1, 5);

                    headerRange.Style.Font.Bold = true;

                    headerRange.Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    headerRange.Style.Alignment.Vertical =
                        ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                    int row = 2;

                    // =========================
                    // Data
                    // =========================
                    foreach (var item in data)
                    {
                        worksheet.Cell(row, 1).Value =
                            item.SupportExecutive ?? "";

                        worksheet.Cell(row, 2).Value =
                            item.TotalAssigned;

                        worksheet.Cell(row, 3).Value =
                            item.Resolved;

                        worksheet.Cell(row, 4).Value =
                            item.Pending;

                        if (item.AverageResolutionTimeHours != null)
                        {
                            worksheet.Cell(row, 5).Value =
                                item.AverageResolutionTimeHours;

                            worksheet.Cell(row, 5)
                                .Style.NumberFormat.Format =
                                "0.00";
                        }
                        else
                        {
                            worksheet.Cell(row, 5).Value =
                                "";
                        }

                        row++;
                    }

                    // =========================
                    // Column Widths
                    // =========================
                    worksheet.Column(1).Width = 25;
                    worksheet.Column(2).Width = 18;
                    worksheet.Column(3).Width = 15;
                    worksheet.Column(4).Width = 15;
                    worksheet.Column(5).Width = 28;

                    // =========================
                    // Alignment
                    // =========================
                    worksheet.Range(
                        1,
                        2,
                        row - 1,
                        5
                    ).Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    worksheet.Range(
                        1,
                        1,
                        row - 1,
                        5
                    ).Style.Alignment.Vertical =
                        ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                    // =========================
                    // Row Height
                    // =========================
                    worksheet.Rows().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "TicketPerformance.xlsx"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message =
                        $"Error exporting Ticket Performance: {ex.Message}"
                });
            }
        }


    }
}