using StaffProj.ApiModels.DTOs.BaseDTOs;
using StaffProj.Domain.Models.Enums;

namespace StaffProj.ApiModels.DTOs.Employee
{
    /// <summary>
    /// Data Transfer Object (DTO) class for representing an employee.
    /// </summary>
    public class EmployeeDTO : ApplicationUserDTO
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets name of the employee.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets Position of the employee.
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Gets or sets Age of the employee.
        /// </summary>
        public uint Age { get; set; }


        /// <summary>
        /// Employee UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the role of the employee.
        /// </summary>
        public Roles UserType { get; set; }
    }
}
