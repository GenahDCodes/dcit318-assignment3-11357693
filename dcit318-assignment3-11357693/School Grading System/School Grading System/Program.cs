using System;
using System.Collections.Generic;
using System.IO;

namespace Q4_StudentGrading
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== Student Grading System (Q4) ===");

            // Example file paths (adjust if needed)
            string inputFile = "students_input.txt";
            string outputFile = "students_report.txt";

            var processor = new StudentResultProcessor();

            try
            {
                // Read students from file
                List<Student> students = processor.ReadStudentsFromFile(inputFile);

                // Write report
                processor.WriteReportToFile(students, outputFile);


                Console.WriteLine($"Report successfully written to '{outputFile}'.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: Input file '{inputFile}' not found.");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
