using StaffProj.CMD.Menu.ConsoleHelpers;
using StaffProj.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StaffProj.CMD.Menu.DBHelpers
{
    internal class AddHelper
    {
        public static Employee GetFromConsole()
        {
            Console.WriteLine("Let's Add a Employee!");
            var name = ConsoleReader<string>.Read("Employee Name");
            var age = ConsoleReader<uint>.Read("Employee Age");
            var position = ConsoleReader<string>.Read("Employee Position");
           
            Employee employee = new Employee()
            {
                Name = name,
                Age = age,
                Position = position,

            };
            return employee;
        }


    }
}
