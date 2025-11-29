using Microsoft.AspNetCore.Mvc;
using HealthcareOCR.Core.Models;
using HealthcareOCR.Services.Services;
using HealthcareOCR.API.Models;

namespace HealthcareOCR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPythonOCRService _ocrService;
        private readonly ILogger<PrescriptionsController> _logger;

        public PrescriptionsController(IPythonOCRService ocrService, ILogger<PrescriptionsController> logger)
        {
            _ocrService = ocrService;
            _logger = logger;
        }

        [HttpGet("test-ocr")]
        public async Task<ActionResult<OCRResult>> TestOCR()
        {
            _logger.LogInformation("Testing OCR integration...");
            var result = await _ocrService.TestOCRAsync();
            
            if (result.Success)
            {
                return Ok(new { 
                    message = "✅ C# to Python OCR integration successful!",
                    data = result 
                });
            }
            else
            {
                return BadRequest(new { 
                    message = "❌ OCR integration failed", 
                    error = result.Error 
                });
            }
        }

        [HttpGet("health")]
        public ActionResult<string> HealthCheck()
        {
            return Ok(new { 
                status = "✅ C# API Running", 
                timestamp = DateTime.UtcNow,
                environment = "GitHub Codespaces",
                framework = ".NET 8"
            });
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetPrescriptions()
        {
            var mockPrescriptions = new[]
            {
                new { 
                    Id = 1, 
                    PatientName = "Raj Kumar", 
                    DoctorName = "Dr. Sharma", 
                    Status = "Processed",
                    Date = DateTime.UtcNow.AddDays(-1)
                },
                new { 
                    Id = 2, 
                    PatientName = "Priya Singh", 
                    DoctorName = "Dr. Verma", 
                    Status = "Processed",
                    Date = DateTime.UtcNow.AddDays(-2)
                }
            };

            return Ok(new {
                message = "Sample prescription data",
                count = mockPrescriptions.Length,
                prescriptions = mockPrescriptions
            });
        }

        [HttpGet("demo")]
        public ActionResult<object> GetDemoData()
        {
            var demoPrescription = new Prescription
            {
                Id = 1,
                PatientName = "Demo Patient",
                DoctorName = "Dr. Demo",
                ExtractedText = "This is a demo prescription extracted via OCR",
                ConfidenceScore = 0.95m,
                PrescriptionDate = DateTime.UtcNow,
                Status = "Demo",
                Medications = new List<Medication>
                {
                    new Medication { DrugName = "Demo Drug", Dosage = "500mg", Frequency = "Twice daily" }
                }
            };

            return Ok(new {
                message = "Demo prescription structure",
                prescription = demoPrescription
            });
        }
    }
}
