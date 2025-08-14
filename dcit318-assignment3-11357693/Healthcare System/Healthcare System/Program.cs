using System;

namespace Q2_HealthcareSystem
{
    public class Program
    {
        public static void Main()
        {
            var app = new HealthSystemApp();

            app.SeedData();
            app.BuildPrescriptionMap();

            app.PrintAllPatients();

            Console.Write("\nEnter a Patient ID to view prescriptions: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                app.PrintPrescriptionsForPatient(id);
            }
            else
            {
                Console.WriteLine("Invalid ID entered.");
            }

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
