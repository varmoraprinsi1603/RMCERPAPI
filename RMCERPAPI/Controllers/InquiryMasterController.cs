using RMCERPAPI.Helpers;
using RMCERPAPI.Models;
using RMCERPAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace RMCERPAPI.Controllers
{
    [Authorize(Roles = "Admin,Support Executive")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InquiryMasterController : ControllerBase
    {
        private readonly InquiryMasterRepository _inquiryMasterRepository;

        public InquiryMasterController(InquiryMasterRepository inquiryMasterRepository)
        {
            _inquiryMasterRepository = inquiryMasterRepository;
        }


        [HttpGet]
        public IActionResult Get(int userid)
        {
            try
            {
                var data = _inquiryMasterRepository.GetInquiryMasterList();

                if (data == null || data.Count == 0)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "No Data Found."
                    });
                }

                EGeneral.ExecuteLogDetail(
                    userid,
                    "InquiryMaster",
                    0,
                    "",
                    "Record Retrieved"
                );

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Successfully Retrieved Data."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error retrieving data: {ex.Message}"
                });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(decimal id, int userid)
        {
            try
            {
                var data = _inquiryMasterRepository.GetDataByID(id);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "No Data Found."
                    });
                }

                EGeneral.ExecuteLogDetail(
                    userid,
                    "InquiryMaster",
                    id,
                    "",
                    "Record By ID Retrieved"
                );

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = $"Successfully Retrieved ID: {id} Data."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error retrieving data by ID: {ex.Message}"
                });
            }
        }


        [HttpPost]
        public IActionResult Save(
            [FromBody] InquiryMasterModel model,
            int userid)
        {
            int result = 0;

            try
            {
                if (model.InquiryID == 0)
                {
                    result =
                        _inquiryMasterRepository.AddInquiryMaster(model);

                    model.InquiryID = result;

                    EGeneral.ExecuteLogDetail(
                        userid,
                        "InquiryMaster",
                        EConverter.ToDecimal(model.InquiryID),
                        EConverter.ToString(model.InquiryID),
                        "Record Insert"
                    );

                    return Ok(new
                    {
                        Status = 1,
                        InquiryID = model.InquiryID,
                        Message = "Record Inserted Successfully."
                    });
                }
                else
                {
                    _inquiryMasterRepository.EditInquiryMaster(model);

                    EGeneral.ExecuteLogDetail(
                        userid,
                        "InquiryMaster",
                        EConverter.ToDecimal(model.InquiryID),
                        EConverter.ToString(model.InquiryID),
                        "Record Update"
                    );

                    return Ok(new
                    {
                        Status = 1,
                        InquiryID = model.InquiryID,
                        Message = "Record Updated Successfully."
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error Saving data: {ex.Message}"
                });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(decimal id, int userid)
        {
            try
            {
                var data =
                    _inquiryMasterRepository.GetDataByID(id);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = $"ID :- {id} not Found."
                    });
                }

                _inquiryMasterRepository.DeleteInquiryMaster(
                    EConverter.ToString(id)
                );

                EGeneral.ExecuteLogDetail(
                    userid,
                    "InquiryMaster",
                    id,
                    EConverter.ToString(id),
                    "Record Deleted"
                );

                return Ok(new
                {
                    Status = 1,
                    Message = "Record Deleted Successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error Deleting data: {ex.Message}"
                });
            }
        }
    }
}