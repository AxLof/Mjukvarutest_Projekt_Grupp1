using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister
{
    public static class CandidateImporter
    {
        public static List<PotentialCandidate> LoadCandidatesFromCsv(string csvPath)
        {
            var candidates = new List<PotentialCandidate>();
            using var reader = new StreamReader(csvPath);
            var header = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (values.Length >= 6)
                {
                    candidates.Add(new PotentialCandidate(
                        values[0], values[1],
                        int.Parse(values[2]),
                        int.Parse(values[3]),
                        values[4], values[5]
                    ));
                }
            }
            return candidates;
        }
    }
}
