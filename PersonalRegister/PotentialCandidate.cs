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
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));
            if (salary < 0)
                throw new ArgumentOutOfRangeException(nameof(salary), "Salary must be zero or positive");
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age), "Age must be greater than zero");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty", nameof(city));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be empty", nameof(address));

            Username = username;
            Password = password;
            Salary = salary;
            Age = age;
            City = city;
            Address = address;
        }
    }
}