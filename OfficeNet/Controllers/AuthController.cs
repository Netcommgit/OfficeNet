﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeNet.Domain.Contracts;
using OfficeNet.Service.Roles;
using OfficeNet.Service.UserService;

namespace OfficeNet.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public AuthController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            try
            {
                var response = await _userService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception exx){
               return BadRequest ($"{exx.Message}");
            }
            
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                var response = await _userService.LoginAsync(request);
                //return Ok(response);
                var exp = DateTime.Now.AddMinutes(2);
                long expUnixTimestamp = ((DateTimeOffset)exp).ToUnixTimeSeconds();
                return Ok(new { id = response.Id, email = response.Email, name = response.FirstName + " " + response.LastName, access_token = response.AccessToken, exp = expUnixTimestamp, expires_in = expUnixTimestamp, refresh_token = response.RefreshToken });
            }
            catch (UnauthorizedAccessException ex) {
                return Ok(new
                {
                    title = "Unauthorized",
                    message = ex.Message,
                    statusCode = 401
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    title = "Internal Server Error",
                    message = ex.Message,
                    statusCode = 500
                });
            }
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _userService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var response = await _userService.RefreshTokenAsync(request);
            //return Ok(response);
            var exp = DateTime.Now.AddMinutes(2);
            long expUnixTimestamp = ((DateTimeOffset)exp).ToUnixTimeSeconds();
            return Ok(new { id = response.Id, email = response.Email, name = response.FirstName + " " + response.LastName, access_token = response.AccessToken, exp = expUnixTimestamp, expires_in = expUnixTimestamp, refresh_token = response.RefreshToken });
        }

        [HttpPost("revoke-refresh-token")]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = await _userService.RevokeRefreshTokenAsync(request);
            if (response != null && response.Message == "Refresh token revoked successfully")
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var response = await _userService.GetCurrentUserAsync();
            return Ok(response);
        }

        [HttpDelete("user/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
        [HttpGet("GetAllUser")]
        [Authorize]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var userList = await _userService.GetUserListAsync();
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewRoleAsync")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddNewRoleAsync(string name)
        {
            try
            {
                var roleName = await _roleService.AddRoleAsync(name);
                return Ok(roleName);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("AddRoleToUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRoleToUser(UserRole objUser)
        {
          var User =  await  _roleService.AssingRoleToUserAync(objUser);
          return Ok(User);
        }
        [HttpGet("GetUserListByPlant")]
        [Authorize]
        public async Task<IActionResult> GetUserListByPlant(int plantId, int departmentId)
        {
            try
            {
                var userList = await _userService.GetUserListByPlantDept(plantId, departmentId);
                var simplifiedresult = userList.Select(u => new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName
                });
                return Ok(simplifiedresult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
