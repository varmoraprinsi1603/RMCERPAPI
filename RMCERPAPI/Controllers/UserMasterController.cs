using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RMCERPAPI.Models;
using RMCERPAPI.Repository;
using Microsoft.Extensions.Configuration;

namespace RMCERPAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly UserMasterRepository _userMasterRepository;
        private readonly IConfiguration _configuration;

        public UserMasterController(
            UserMasterRepository userMasterRepository,
            IConfiguration configuration)
        {
            _userMasterRepository = userMasterRepository;
            _configuration = configuration;
        }


        // =========================================================
        // LOGIN
        // =========================================================
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserMasterModel model)
        {
            try
            {
                var data = _userMasterRepository.Login(
                    model.UserName,
                    model.Password
                );

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Invalid Username or Password."
                    });
                }

                var claims = new[]
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        data.UserID.ToString()
                    ),

                    new Claim(
                        ClaimTypes.Name,
                        data.UserName
                    ),

                    new Claim(
                        ClaimTypes.Role,
                        data.RoleName
                    ),

                    new Claim(
                        "RoleID",
                        data.RoleID.ToString()
                    )
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["JwtSettings:Key"]
                    )
                );

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(
                        Convert.ToDouble(
                            _configuration["JwtSettings:ExpiryMinutes"]
                        )
                    ),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler()
                    .WriteToken(token);

                return Ok(new
                {
                    Data = data,
                    Token = tokenString,
                    Status = 1,
                    Message = "Login Successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error Login data: {ex.Message}"
                });
            }
        }


        // =========================================================
        // GET ALL USERS
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUserList()
        {
            try
            {
                var data = _userMasterRepository.GetUserList();

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "User list fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching user list: {ex.Message}"
                });
            }
        }


        // =========================================================
        // GET USER BY ID
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUserByID(decimal UserID)
        {
            try
            {
                var data = _userMasterRepository.GetUserByID(UserID);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "User not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "User fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching user: {ex.Message}"
                });
            }
        }


        // =========================================================
        // ADD USER
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddUser([FromBody] UserMasterModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid user data."
                    });
                }

                decimal userID =
                    _userMasterRepository.AddUser(model);

                return Ok(new
                {
                    UserID = userID,
                    Status = 1,
                    Message = "User added successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error adding user: {ex.Message}"
                });
            }
        }


        // =========================================================
        // UPDATE USER
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserMasterModel model)
        {
            try
            {
                if (model == null || model.UserID <= 0)
                {
                    return BadRequest(new
                    {
                        Status = 0,
                        Message = "Invalid user data."
                    });
                }

                var data =
                    _userMasterRepository.UpdateUser(model);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "User not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "User updated successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error updating user: {ex.Message}"
                });
            }
        }


        // =========================================================
        // DEACTIVATE USER
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeactivateUser(decimal UserID)
        {
            try
            {
                var data =
                    _userMasterRepository.DeactivateUser(UserID);

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "User not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "User deactivated successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error deactivating user: {ex.Message}"
                });
            }
        }


        // =========================================================
        // GET ACTIVE ROLES
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                var data = _userMasterRepository.GetRoles();

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Roles fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching roles: {ex.Message}"
                });
            }
        }
    }
}