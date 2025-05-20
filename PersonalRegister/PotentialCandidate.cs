using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister
{
    public class PotentialCandidate
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public PotentialCandidate(string username, string password, int salary, int age, string city, string address)
        {
            Username = username;
            Password = password;
            Salary = salary;
            Age = age;
            City = city;
            Address = address;
        }
    }
}
