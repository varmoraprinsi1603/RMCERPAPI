using RMCERPAPI.Helpers;
using RMCERPAPI.Models;
using RMCERPAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMCERPAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _EmployeeRepository;
        public EmployeeController(EmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        [HttpGet]
        public IActionResult Get(int userid)
        {
            try
            {
                var data = _EmployeeRepository.GetEmployeeList();
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
                var data = _EmployeeRepository.GetDataByID(id);
                if (data == null)
                {
                    return NotFound(new { Status = 0, Message = "No Data Found." });
                }

                EGeneral.ExecuteLogDetail(userid, "", id, "", "Record By ID Retrieved");
                return Ok(new { Data = data, Status = 1, Message = $"Successfully Retrieved ID: {id} Data." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Message = $"Error retrieving data by ID: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody] EmployeeModel model, int userid)
        {
            int result = 0;
            try
            {
                if (model.EmployeeID == 0)
                {
                    result = _EmployeeRepository.AddEmployee(model);
                    model.EmployeeID = result;
                    EGeneral.ExecuteLogDetail(userid, "Employee", EConverter.ToDecimal(model.EmployeeID), EConverter.ToString(model.EmployeeID), "Record Insert");
                    return Ok(new { Status = 1, Message = "Record Inserted Successfully." });
                }
                else
                {
                    _EmployeeRepository.EditEmployee(model);
                    EGeneral.ExecuteLogDetail(userid, "Employee", EConverter.ToDecimal(model.EmployeeID), EConverter.ToString(model.EmployeeID), "Record Update");
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
                var data = _EmployeeRepository.GetDataByID(id);
                if (data == null)
                {
                    return NotFound(new { Status = 0, Message = $"ID :- {id} not Found." });
                }

                _EmployeeRepository.DeleteEmployee(EConverter.ToString(id));
                EGeneral.ExecuteLogDetail(userid, "Employee", EConverter.ToDecimal(id), EConverter.ToString(id), "Record Deleted");
                return Ok(new { Status = 1, Message = "Record Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Message = $"Error Deleting data: {ex.Message}" });
            }
        }
    }
}

