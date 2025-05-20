using System.Data;
using System.Data.SQLite;

namespace PersonalRegister
{
    public class DatabaseHelper
    {
        private static string dbPath = "Data Source=PersonalRegister.db;Version=3;Pooling=False;";

        public void InitializeDatabase()
        {
            if (!File.Exists("PersonalRegister.db"))
            {
                SQLiteConnection.CreateFile("PersonalRegister.db");

                using (var connection = new SQLiteConnection(dbPath))
                {
                    connection.Open();
                    string createTableQuery = @"
                        CREATE TABLE Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            UniqueID TEXT NOT NULL,
                            Role TEXT NOT NULL
                        )";
                    SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreateCandidateTable()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Open();
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Candidates (
                            Username TEXT PRIMARY KEY,
                            Password TEXT,
                            Salary INTEGER,
                            Age INTEGER,
                            City TEXT,
                            Address TEXT
                        );";
            // using var cmd = new SQLiteCommand(sql, connection);
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(dbPath);
        }

        public static void ExecuteNonQuery(string query, params SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string query, params SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public void InsertCandidate(PotentialCandidate candidate)
        {
            string sql = @"INSERT OR REPLACE INTO Candidates (Username, Password, Salary, Age, City, Address) 
                           VALUES (@Username, @Password, @Salary, @Age, @City, @Address)";
            var connection = new SQLiteConnection(dbPath);
            connection.Open();
            using var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Username", candidate.Username);
            cmd.Parameters.AddWithValue("@Password", candidate.Password);
            cmd.Parameters.AddWithValue("@Salary", candidate.Salary);
            cmd.Parameters.AddWithValue("@Age", candidate.Age);
            cmd.Parameters.AddWithValue("@City", candidate.City);
            cmd.Parameters.AddWithValue("@Address", candidate.Address);
            cmd.ExecuteNonQuery();
        }

        public List<PotentialCandidate> GetAllCandidates()
        {
            var candidates = new List<PotentialCandidate>();
            string sql = "SELECT * FROM Candidates";
            var connection = new SQLiteConnection(dbPath);
            using var cmd = new SQLiteCommand(sql, connection);
            connection.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                candidates.Add(new PotentialCandidate(
                    reader["Username"].ToString(),
                    reader["Password"].ToString(),
                    int.Parse(reader["Salary"].ToString()),
                    int.Parse(reader["Age"].ToString()),
                    reader["City"].ToString(),
                    reader["Address"].ToString()
                ));
            }
            return candidates;
        }

        public void UpdateCandidate(PotentialCandidate candidate)
        {
            string sql = @"UPDATE Candidates SET 
                            Password = @Password, 
                            Salary = @Salary, 
                            Age = @Age, 
                            City = @City, 
                            Address = @Address 
                           WHERE Username = @Username";
            var connection = new SQLiteConnection(dbPath);
            connection.Open();
            using var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Password", candidate.Password);
            cmd.Parameters.AddWithValue("@Salary", candidate.Salary);
            cmd.Parameters.AddWithValue("@Age", candidate.Age);
            cmd.Parameters.AddWithValue("@City", candidate.City);
            cmd.Parameters.AddWithValue("@Address", candidate.Address);
            cmd.Parameters.AddWithValue("@Username", candidate.Username);
            cmd.ExecuteNonQuery();
        }

        public void DeleteCandidate(string username)
        {
            string sql = "DELETE FROM Candidates WHERE Username = @Username";
            var connection = new SQLiteConnection(dbPath);
            connection.Open();
            using var cmd = new SQLiteCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.ExecuteNonQuery();
        }
    }
}
