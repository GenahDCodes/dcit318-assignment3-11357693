using System;

namespace Q2_HealthcareSystem
{
    public class Prescription
    {
        public int Id { get; }
        public int PatientId { get; }
        public string MedicationName { get; }
        public DateTime DateIssued { get; }

        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName;
            DateIssued = dateIssued;
        }

        public override string ToString() =>
            $"Prescription ID: {Id}, PatientID: {PatientId}, Medication: {MedicationName}, Date: {DateIssued:d}";
    }
}
