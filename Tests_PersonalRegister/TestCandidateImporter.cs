using PersonalRegister;
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
        public void TestCorrectCsvData_ReturnCorrectCandidateList()
        {
            // Simulate csv data
            string csv =    "Username,Password,Salary,Age,City,Address\n" +
                            "fbreston0,xM7>@GE4QR0Dxlp,38433,13,Yuannan,29 Nelson Park\n" +
                            "nboanas1,mI6jN65R9Yg8jt,63391,5,Melbourne,05 Kinsman Park";

            // Creates a temporary file
            string path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            // Imports and runs the temporary file in 'LoadCandidatesFromCsv'
            var result = CandidateImporter.LoadCandidatesFromCsv(path);

            // Verifying the row count
            Assert.Equal(2, result.Count);
            // Verifying the Username on row 1
            Assert.Equal("fbreston0", result[0].Username);
            // Verifying the Age on row 2
            Assert.Equal(5, result[1].Age);

            // Deletes the temporary file
            File.Delete(path);
        }

        [Fact]
        public void TestHeaderOnlyInput_ReturnEmptyList()
        {
            // Simulate csv header
            string csv = "Username,Password,Salary,Age,City,Address\n";

            // Creates a temporary file
            string path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            // Imports and runs the temporary file in 'LoadCandidatesFromCsv'
            var result = CandidateImporter.LoadCandidatesFromCsv(path);

            // Verifying if empty
            Assert.Empty(result);

            // Deletes the temporary file
            File.Delete(path);
        }

        [Fact]
        public void TestIncompleteRow_ReturnIncompleteList()
        {
            // Simulate csv data
            string csv =    "Username,Password,Salary,Age,City,Address\n" +
                            "incomplete,user,shortrow\n" +
                            "nboanas1,mI6jN65R9Yg8jt,63391,5,Melbourne,05 Kinsman Park";

            // Creates a temporary file
            string path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            // Imports and runs the temporary file in 'LoadCandidatesFromCsv'
            var result = CandidateImporter.LoadCandidatesFromCsv(path);

            // Verifying that only one of the rows succeeds
            Assert.Single(result);
            Assert.Equal("nboanas1", result[0].Username);

            // Deletes the temporary file
            File.Delete(path);
        }

        [Fact]
        public void TestInvalidNumberFormat_ReturnFormatException()
        {
            // Simulate csv data
            string csv = "Username,Password,Salary,Age,City,Address\n" +
                            "error_data,password123,notaNumber,NaN,NiceCity,Home";

            // Creates a temporary file
            string path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            var exception = Record.Exception(() => CandidateImporter.LoadCandidatesFromCsv(path));
            Assert.IsType<FormatException>(exception);

            // Deletes the temporary file
            File.Delete(path);
        }

        [Fact]
        public void TestMissingFile_ReturnFileNotFoundException()
        {
            string path = "unvalidpath.csv";

            var exception = Record.Exception(() => CandidateImporter.LoadCandidatesFromCsv(path));
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact]
        public void TestWithAdditionalColumns_ReturnsValidColumnsOnly()
        {
            // Simulate csv data
            string csv =    "Username,Password,Salary,Age,City,Address\n" +
                            "fbreston0,xM7>@GE4QR0Dxlp,38433,13,Yuannan,29 Nelson Park, ExtraColumn";

            // Creates a temporary file
            string path = Path.GetTempFileName();
            File.WriteAllText(path, csv);

            // Imports and runs the temporary file in 'LoadCandidatesFromCsv'
            var result = CandidateImporter.LoadCandidatesFromCsv(path);

            Assert.Single(result);
            Assert.Equal("fbreston0", result[0].Username);
            Assert.Equal("29 Nelson Park", result[0].Address);

            // Deletes the temporary file
            File.Delete(path);
        }
    }
}
