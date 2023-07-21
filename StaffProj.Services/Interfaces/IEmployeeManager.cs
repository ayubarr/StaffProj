using StaffProj.ApiModels.DTOs.Employee;
using StaffProj.ApiModels.Reponse.Interfaces;
using StaffProj.Domain.Models.Entities;
using StaffProj.Domain.Models.Enums;

namespace StaffProj.Services.Interfaces
{
    /// <summary>
    /// Service for managing employee entities and their roles, projects, and tasks.
    /// </summary>
    public interface IEmployeeManager
    {
        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the list of employees or an error if the operation fails.</returns>
        Task<IBaseResponse<IEnumerable<Employee>>> GetAllAsync();

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the employee object or an error if the operation fails.</returns>
        Task<IBaseResponse<Employee>> GetByIdAsync(string employeeId);

        /// <summary>
        /// Updates the details of the specified employee.
        /// </summary>
        /// <param name="employeeDto">The employee object containing the updated details.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> UpdateAsync(string id, UpdateEmployeeDTO employeeDto);

        /// <summary>
        /// Deletes the specified employee by their ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> DeleteByIdAsync(string employeeId);

        /// <summary>
        /// Sets the specified employee as an Role.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <param name="roleType">The type of role to assign.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> SetEmployeeNewRoleByIdAsync(string employeeId, Roles roleType);
        /// <summary>
        /// Checks the specified employee as an Role.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="role">The type of role to assign</param>
        /// <returns></returns>
        Task<IBaseResponse<string>> CheckUserRole(string userId, Roles role);
    }
}
