using System;
using System.Collections.Generic;
using System.IO;

namespace Q4_StudentGrading
{
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();

            using (var reader = new StreamReader(inputFilePath))
            {
                string? line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // skip empty lines

                    var parts = line.Split(',');

                    if (parts.Length != 3)
                        throw new MissingFieldException($"Line {lineNumber}: Missing fields.");

                    if (!int.TryParse(parts[0].Trim(), out int id))
                        throw new FormatException($"Line {lineNumber}: Invalid student ID format.");

                    string fullName = parts[1].Trim();

                    if (!int.TryParse(parts[2].Trim(), out int score))
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format.");

                    if (score < 0 || score > 100)
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Score out of range (0-100).");

                    students.Add(new Student(id, fullName, score));
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine(student);
                }
            }
        }
    }
}
