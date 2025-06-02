using Moq;
using PersonalRegister;
using PersonalRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_PersonalRegister
{
    public class TestCandidateImporter
    {
        [Fact]
        public void TestValidCsv_ReturnCorrectCandidateList()
        {
            string csv = "Username,Password,Salary,Age,City,Address\n" +
                            "user1,pass1,10001,21,city1,address1\n";
            var mockReader = new Mock<IStreamReaderHelper>();
            mockReader.Setup(f => f.Create(It.IsAny<string>())).Returns(new StringReader(csv));

            var result = CandidateImporter.LoadCandidatesFromCsv("dummy.csv", mockReader.Object);

            Assert.Single(result);
            Assert.Equal("user1", result[0].Username);
            Assert.Equal("pass1", result[0].Password);
            Assert.Equal(10001, result[0].Salary);
            Assert.Equal(21, result[0].Age);
            Assert.Equal("city1", result[0].City);
            Assert.Equal("address1", result[0].Address);
        }

        [Fact]
        public void TestHeaderOnlyInput_ReturnEmptyList()
        {
            string csv = "Username,Password,Salary,Age,City,Address\n";
            var mockReader = new Mock<IStreamReaderHelper>();
            mockReader.Setup(f => f.Create(It.IsAny<string>())).Returns(new StringReader(csv));

            var result = CandidateImporter.LoadCandidatesFromCsv("dummy.csv", mockReader.Object);

            Assert.Empty(result);
        }
        
        [Fact]
        public void TestIncompleteRow_ReturnIncompleteList()
        {
            string csv =    "Username,Password,Salary,Age,City,Address\n" +
                            "incomplete,user,shortrow\n" +
                            "nboanas1,mI6jN65R9Yg8jt,63391,5,Melbourne,05 Kinsman Park";
            var mockReader = new Mock<IStreamReaderHelper>();
            mockReader.Setup(f => f.Create(It.IsAny<string>())).Returns(new StringReader(csv));

            var result = CandidateImporter.LoadCandidatesFromCsv("dummy.csv", mockReader.Object);

            Assert.Single(result);
            Assert.Equal("nboanas1", result[0].Username);
        }

        [Fact]
        public void TestInvalidNumberFormat_ReturnFormatException()
        {
            string csv = "Username,Password,Salary,Age,City,Address\n" +
                            "error_data,password123,notaNumber,NaN,NiceCity,Home";
            var mockReader = new Mock<IStreamReaderHelper>();
            mockReader.Setup(f => f.Create(It.IsAny<string>())).Returns(new StringReader(csv));

            var exception = Record.Exception(() => CandidateImporter.LoadCandidatesFromCsv("dummy.csv", mockReader.Object));
            Assert.IsType<FormatException>(exception);
        }

        [Fact]
        public void TestWithAdditionalColumns_ReturnsValidColumnsOnly()
        {
            string csv =    "Username,Password,Salary,Age,City,Address\n" +
                            "fbreston0,xM7>@GE4QR0Dxlp,38433,13,Yuannan,29 Nelson Park, ExtraColumn";
            var mockReader = new Mock<IStreamReaderHelper>();
            mockReader.Setup(f => f.Create(It.IsAny<string>())).Returns(new StringReader(csv));

            var result = CandidateImporter.LoadCandidatesFromCsv("dummy.csv", mockReader.Object);

            Assert.Single(result);
            Assert.Equal("fbreston0", result[0].Username);
            Assert.Equal("29 Nelson Park", result[0].Address);
        }
    }
}
