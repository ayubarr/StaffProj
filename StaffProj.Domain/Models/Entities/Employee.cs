using StaffProj.Domain.Models.Abstractions.BaseUsers;
using StaffProj.Domain.Models.Enums;

namespace StaffProj.Domain.Models.Entities
{
    public class Employee : ApplicationUser
    {
        public string Name { get; set; }
        public uint  Age { get; set; }
        public string Position { get; set; }
        public Roles Role { get; set; }
    }
}
