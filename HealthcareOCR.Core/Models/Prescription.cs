using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthcareOCR.Core.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        
        [Required]
        public string PatientName { get; set; } = string.Empty;
        
        public string DoctorName { get; set; } = string.Empty;
        public DateTime PrescriptionDate { get; set; } = DateTime.UtcNow;
        public string ExtractedText { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public decimal ConfidenceScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Processed";
        
        public List<Medication> Medications { get; set; } = new List<Medication>();
    }

    public class Medication
    {
        public int Id { get; set; }
        
        [Required]
        public string DrugName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; } = null!;
    }

    public class OCRResult
    {
        public bool Success { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Confidence { get; set; }
        public List<Medication> Medications { get; set; } = new List<Medication>();
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
