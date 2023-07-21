namespace StaffProj.ApiModels.DTOs.Employee
{
    public class UpdateEmployeeDTO
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
    }
}
