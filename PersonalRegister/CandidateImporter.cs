using PersonalRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalRegister
{
    public class StreamReaderHelper : IStreamReaderHelper
    {
        public TextReader Create(string path) => new StreamReader(path);
    }

    public static class CandidateImporter
    {
        public static List<PotentialCandidate> LoadCandidatesFromCsv(string csvPath, IStreamReaderHelper readerHelper)
        {
            var candidates = new List<PotentialCandidate>();
            using var reader = readerHelper.Create(csvPath);
            var header = reader.ReadLine();
            while (reader.Peek() != -1)
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
