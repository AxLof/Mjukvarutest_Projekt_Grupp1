namespace PersonalRegister
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var db = new DatabaseHelper();
            db.InitializeDatabase();
            db.CreateCandidateTable();

            var candidates = CandidateImporter.LoadCandidatesFromCsv("random_user_data.csv");
            foreach (var c in candidates)
            {
                db.InsertCandidate(c);
            }

            foreach (var c in db.GetAllCandidates())
            {
                Console.WriteLine($"{c.Username}, {c.Age}, {c.City}");
            }
            Application.Run(new LoginForm());
        }

    }
}