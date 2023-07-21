using StaffProj.ApiModels.DTOs.BaseDTOs;

namespace StaffProj.ApiModels.DTOs.Employee
{
    public class UpdateEmployeeDTO : ApplicationUserDTO
    {
        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the middle name of the employee.
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

    }
}
