using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffProj.ApiModels.Auth.Models;
using StaffProj.ApiModels.DTOs.Employee;
using StaffProj.Domain.Models.Entities;
using StaffProj.Domain.Models.Enums;
using StaffProj.Services.Interfaces;

namespace StaffProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeService;
        private readonly IAuthManager<Employee> _authService;

        public EmployeeController(IEmployeeManager employeeService, IAuthManager<Employee> authService)
        {
            _employeeService = employeeService;
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.Login(model);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);
            await _employeeService.SetEmployeeNewRoleByIdAsync(result.Data, Roles.Employee);

            return Ok(result);
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetAllAsync();


            var employeeDtos = response.Data.Select(employee => new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Age = employee.Age,
                UserName = employee.UserName,
                Email = employee.Email,
            });

            return Ok(employeeDtos);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _employeeService.GetByIdAsync(id);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateEmployeeDTO employeeDto)
        {
            var response = await _employeeService.UpdateAsync(id, employeeDto);
            return Ok(response.Data);
        }



        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _employeeService.DeleteByIdAsync(id);
            return Ok(response);
        }
   
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Supervisor")]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var result = await _authService.RefreshToken(tokenModel);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Supervisor")]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            try
            {
                await _authService.RevokeRefreshTokenByUsernameAsync(username);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Supervisor")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _authService.RevokeAllRefreshTokensAsync();
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("checkUserRole/{userId}/{roleType}")]
        public async Task<IActionResult> CheckUserRole(string userId, Roles roleType)
        {
            var response = await _employeeService.CheckUserRole(userId, roleType);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        [Route("putRoleById/{userId}/{roleType}")]
        public async Task<IActionResult> PutRoleById(string userId, Roles roleType)
        {
            var response = await _employeeService.SetEmployeeNewRoleByIdAsync(userId, roleType);
            return Ok(response);
        }


    }
}
