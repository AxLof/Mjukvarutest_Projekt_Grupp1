using PersonalRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_PersonalRegister
{
    public class TestPotentialCandidate
    {
        [Fact]
        public void TestValidInput_CreatesObjectCorrectly()
        {
            var candidate = new PotentialCandidate("fbreston0", "xM7>@GE4QR0Dxlp", 38433, 13, "Yuannan", "29 Nelson Park");

            Assert.Equal("fbreston0", candidate.Username);
            Assert.Equal("xM7>@GE4QR0Dxlp", candidate.Password);
            Assert.Equal(38433, candidate.Salary);
            Assert.Equal(13, candidate.Age);
            Assert.Equal("Yuannan", candidate.City);
            Assert.Equal("29 Nelson Park", candidate.Address);
        }

        [Fact]
        public void TestEmptyUsername_ReturnArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new PotentialCandidate("", "xM7>@GE4QR0Dxlp", 38433, 13, "Yuannan", "29 Nelson Park"));
            Assert.Equal("username", exception.ParamName);
        }

        [Fact]
        public void TestEmptyPassword_ReturnArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new PotentialCandidate("fbreston0", "", 38433, 13, "Yuannan", "29 Nelson Park"));
            Assert.Equal("password", exception.ParamName);
        }

        [Fact]
        public void TestNegativeSalary_ReturnArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new PotentialCandidate("fbreston0", "xM7>@GE4QR0Dxlp", -38433, 13, "Yuannan", "29 Nelson Park"));
            Assert.Equal("salary", exception.ParamName);
        }

        [Fact]
        public void TestNegativeAge_ReturnArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new PotentialCandidate("fbreston0", "xM7>@GE4QR0Dxlp", 38433, -13, "Yuannan", "29 Nelson Park"));
            Assert.Equal("age", exception.ParamName);
        }

        [Fact]
        public void TestEmptyCity_ReturnArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new PotentialCandidate("fbreston0", "xM7>@GE4QR0Dxlp", 38433, 13, "", "29 Nelson Park"));
            Assert.Equal("city", exception.ParamName);
        }

        [Fact]
        public void TestEmptyAddress_ReturnArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new PotentialCandidate("fbreston0", "xM7>@GE4QR0Dxlp", 38433, 13, "Yuannan", ""));
            Assert.Equal("address", exception.ParamName);
        }
    }
}
