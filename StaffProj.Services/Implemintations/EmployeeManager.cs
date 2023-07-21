using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaffProj.ApiModels.DTOs.Employee;
using StaffProj.ApiModels.Reponse.Helpers;
using StaffProj.ApiModels.Reponse.Interfaces;
using StaffProj.Domain.Models.Entities;
using StaffProj.Domain.Models.Enums;
using StaffProj.Services.Interfaces;
using StaffProj.Services.Mapping.Helpers;
using StaffProj.ValidationHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProj.Services.Implemintations
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeManager(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IBaseResponse<IEnumerable<Employee>>> GetAllAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                ObjectValidator<IEnumerable<Employee>>.CheckIsNotNullObject(users);

                return ResponseFactory<IEnumerable<Employee>>.CreateSuccessResponse(users);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<IEnumerable<Employee>>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<Employee>>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<Employee>> GetByIdAsync(string employeeId)
        {
            try
            {
                StringValidator.CheckIsNotNull(employeeId);

                var user = await _userManager.FindByIdAsync(employeeId);
                ObjectValidator<Employee>.CheckIsNotNullObject(user);

                return ResponseFactory<Employee>.CreateSuccessResponse(user);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<Employee>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<Employee>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> UpdateAsync(string id, UpdateEmployeeDTO employeeDto)
        {
            try
            {
                ObjectValidator<UpdateEmployeeDTO>.CheckIsNotNullObject(employeeDto);

                var employee = await _userManager.FindByIdAsync(id);
                if (employee is null)
                    throw new ArgumentNullException("User Not found");

                //employee = MapperHelperForUser<UpdateEmployeeDTO, Employee>.Map(employeeDto);

                employee.Name = employeeDto.Name;
                employee.Position = employeeDto.Position;
                employee.Age = employeeDto.Age;
                employee.Email = employeeDto.Email;
                employee.UserName = employeeDto.UserName;

                await _userManager.UpdateAsync(employee);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteByIdAsync(string employeeId)
        {
            try
            {
                StringValidator.CheckIsNotNull(employeeId);

                var employee = await _userManager.FindByIdAsync(employeeId);
                ObjectValidator<Employee>.CheckIsNotNullObject(employee);

                var result = await _userManager.DeleteAsync(employee);
                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Failed to delete user."));
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<string>> CheckUserRole(string userId, Roles role)
        {
            try
            {
                StringValidator.CheckIsNotNull(userId);

                var user = await _userManager.FindByIdAsync(userId);
                ObjectValidator<Employee>.CheckIsNotNullObject(user);

                var roleName = role.ToString();

                bool isInRole = await _userManager.IsInRoleAsync(user, roleName);

                if (isInRole)
                {
                    return ResponseFactory<string>.CreateSuccessResponse($"User role is: {roleName}");
                }
                else
                {
                    throw new ArgumentNullException("it's role not found");
                }
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<string>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception ex)
            {
                return ResponseFactory<string>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> SetEmployeeNewRoleByIdAsync(string employeeId, Roles roleType)
        {
            try
            {
                StringValidator.CheckIsNotNull(employeeId);

                var employee = await _userManager.FindByIdAsync(employeeId);
                ObjectValidator<Employee>.CheckIsNotNullObject(employee);
                List<string> roles = new List<string>()
                {
                    Roles.Employee.ToString(),
                    Roles.Manager.ToString(),
                    Roles.Admin.ToString(),
                };

                await _userManager.RemoveFromRolesAsync(employee, roles);

                var result = await _userManager.AddToRoleAsync(employee, roleType.ToString());

                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    throw new Exception("Failed to set user as role.");
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }
    }
}
