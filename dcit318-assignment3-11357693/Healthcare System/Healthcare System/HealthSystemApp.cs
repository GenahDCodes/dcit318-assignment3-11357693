using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2_HealthcareSystem
{
    public class HealthSystemApp
    {
        private readonly Repository<Patient> _patientRepo = new();
        private readonly Repository<Prescription> _prescriptionRepo = new();
        private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new();

        public void SeedData()
        {
            // Add Patients
            _patientRepo.Add(new Patient(1, "Alice Smith", 30, "Female"));
            _patientRepo.Add(new Patient(2, "John Doe", 45, "Male"));
            _patientRepo.Add(new Patient(3, "Mary Johnson", 28, "Female"));

            // Add Prescriptions
            _prescriptionRepo.Add(new Prescription(1, 1, "Paracetamol", DateTime.Today.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(2, 1, "Amoxicillin", DateTime.Today.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(3, 2, "Ibuprofen", DateTime.Today.AddDays(-7)));
            _prescriptionRepo.Add(new Prescription(4, 3, "Cetirizine", DateTime.Today.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(5, 3, "Vitamin C", DateTime.Today));
        }

        public void BuildPrescriptionMap()
        {
            _prescriptionMap.Clear();
            foreach (var prescription in _prescriptionRepo.GetAll())
            {
                if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                {
                    _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                }
                _prescriptionMap[prescription.PatientId].Add(prescription);
            }
        }

        public void PrintAllPatients()
        {
            Console.WriteLine("=== Patients ===");
            foreach (var patient in _patientRepo.GetAll())
            {
                Console.WriteLine(patient);
            }
        }

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            return _prescriptionMap.ContainsKey(patientId)
                ? _prescriptionMap[patientId]
                : new List<Prescription>();
        }

        public void PrintPrescriptionsForPatient(int id)
        {
            var prescriptions = GetPrescriptionsByPatientId(id);
            if (!prescriptions.Any())
            {
                Console.WriteLine("No prescriptions found for this patient.");
                return;
            }

            Console.WriteLine($"=== Prescriptions for Patient ID {id} ===");
            foreach (var prescription in prescriptions)
            {
                Console.WriteLine(prescription);
            }
        }
    }
}
