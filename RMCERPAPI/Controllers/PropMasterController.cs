//using RMCERPAPI.Helpers;
//using RMCERPAPI.Models;
//using RMCERPAPI.Repository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RMCERPAPI.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class PropMasterController : ControllerBase
//    {
//        private readonly PropMasterRepository _propMasterRepository;
//        public PropMasterController(PropMasterRepository propMasterRepository)
//        {
//            _propMasterRepository = propMasterRepository;
//        }

//        [HttpGet]
//        public IActionResult Get(int userid)
//        {
//            try
//            {
//                var data = _propMasterRepository.GetPropMasterList();
//                if (data == null || data.Count == 0)
//                {
//                    return NotFound(new { Status = 0, Message = "No Data Found." });
//                }

//                EGeneral.ExecuteLogDetail(userid, "PropMaster", 0, "", "Record Retrieved");
//                return Ok(new { Data = data, Status = 1, Message = "Successfully Retrieved Data." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Status = 0, Message = ex.ToString() });
//            }
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetById(decimal id, int userid)
//        {
//            try
//            {
//                var data = _propMasterRepository.GetDataByID(id);
//                if (data == null)
//                {
//                    return NotFound(new { Status = 0, Message = "No Data Found." });
//                }

//                EGeneral.ExecuteLogDetail(userid, "PropMaster", id, "", "Record By ID Retrieved");
//                return Ok(new { Data = data, Status = 1, Message = $"Successfully Retrieved ID: {id} Data." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Status = 0, Message = $"Error retrieving data by ID: {ex.Message}" });
//            }
//        }

//        [HttpPost]
//        public IActionResult Save([FromBody] PropMasterModel model, int userid)
//        {
//            int result = 0;
//            try
//            {
//                if (model.PropID == 0)
//                {
//                    result = _propMasterRepository.AddPropMaster(model);
//                    model.PropID = result;
//                    EGeneral.ExecuteLogDetail(userid, "PropMaster", EConverter.ToDecimal(model.PropID), EConverter.ToString(model.PropID), "Record Insert");
//                    return Ok(new { Status = 1, Message = "Record Inserted Successfully." });
//                }
//                else
//                {
//                    _propMasterRepository.EditPropMaster(model);
//                    EGeneral.ExecuteLogDetail(userid, "PropMaster", EConverter.ToDecimal(model.PropID), EConverter.ToString(model.PropID), "Record Update");
//                    return Ok(new { Status = 1, Message = "Record Updated Successfully." });
//                }
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Status = 0, Message = $"Error Saving data: {ex.Message}" });
//            }
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(decimal id, int userid)
//        {
//            try
//            {
//                var data = _propMasterRepository.GetDataByID(id);
//                if (data == null)
//                {
//                    return NotFound(new { Status = 0, Message = $"ID :- {id} not Found." });
//                }

//                _propMasterRepository.DeletePropMaster(EConverter.ToString(id));
//                EGeneral.ExecuteLogDetail(userid, "PropMaster", EConverter.ToDecimal(id), EConverter.ToString(id), "Record Deleted");
//                return Ok(new { Status = 1, Message = "Record Deleted Successfully." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Status = 0, Message = $"Error Deleting data: {ex.Message}" });
//            }
//        }
//    }
//}


using RMCERPAPI.Helpers;
using RMCERPAPI.Models;
using RMCERPAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RMCERPAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropMasterController : ControllerBase
    {
        private readonly PropMasterRepository _propMasterRepository;
        public PropMasterController(PropMasterRepository propMasterRepository)
        {
            _propMasterRepository = propMasterRepository;
        }

        [HttpGet]
        public IActionResult Get(int userid)
        {
            try
            {
                var data = _propMasterRepository.GetPropMasterList();
                if (data == null || data.Count == 0)
                {
                    return NotFound(new { Status = 0, Message = "No Data Found." });
                }

                //EGeneral.ExecuteLogDetail(userid, "PropMaster", 0, "", "Record Retrieved");
                return Ok(new { Data = data, Status = 1, Message = "Successfully Retrieved Data." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Message = $"Error retrieving data: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(decimal id, int userid)
        {
            try
            {
                var data = _propMasterRepository.GetDataByID(id);
                if (data == null)
                {
                    return NotFound(new { Status = 0, Message = "No Data Found." });
                }

                EGeneral.ExecuteLogDetail(userid, "PropMaster", id, "", "Record By ID Retrieved");
                return Ok(new { Data = data, Status = 1, Message = $"Successfully Retrieved ID: {id} Data." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Message = $"Error retrieving data by ID: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody] PropMasterModel model, int userid)
        {
            int result = 0;
            try
            {
                if (model.PropID == 0)
                {
                    result = _propMasterRepository.AddPropMaster(model);
                    model.PropID = result;
                    EGeneral.ExecuteLogDetail(userid, "PropMaster", EConverter.ToDecimal(model.PropID), EConverter.ToString(model.PropID), "Record Insert");
                    return Ok(new { Status = 1, Message = "Record Inserted Successfully." });
                }
                else
                {
                    _propMasterRepository.EditPropMaster(model);
                    EGeneral.ExecuteLogDetail(userid, "PropMaster", EConverter.ToDecimal(model.PropID), EConverter.ToString(model.PropID), "Record Update");
                    return Ok(new { Status = 1, Message = "Record Updated Successfully." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Message = $"Error Saving data: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(decimal id, int userid)
        {
            try
            {
                var data = _propMasterRepository.GetDataByID(id);
                if (data == null)
                {
                    return NotFound(new { Status = 0, Message = $"ID :- {id} not Found." });
                }

                _propMasterRepository.DeletePropMaster(EConverter.ToString(id));
                EGeneral.ExecuteLogDetail(userid, "PropMaster", EConverter.ToDecimal(id), EConverter.ToString(id), "Record Deleted");
                return Ok(new { Status = 1, Message = "Record Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Message = $"Error Deleting data: {ex.Message}" });
            }
        }
    }
}

