using Microsoft.AspNetCore.Http;

namespace HealthcareOCR.API.Models
{
    public class PrescriptionCreateRequest
    {
        public IFormFile? ImageFile { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
    }
}
