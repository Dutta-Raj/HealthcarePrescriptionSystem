using System.Net.Http.Json;
using HealthcareOCR.Core.Models;
using Microsoft.Extensions.Logging;

namespace HealthcareOCR.Services.Services
{
    public interface IPythonOCRService
    {
        Task<OCRResult> TestOCRAsync();
    }

    public class PythonOCRService : IPythonOCRService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PythonOCRService> _logger;
        
        public PythonOCRService(HttpClient httpClient, ILogger<PythonOCRService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<OCRResult> TestOCRAsync()
        {
            try
            {
                _logger.LogInformation("Testing Python OCR service...");
                var response = await _httpClient.GetAsync("http://localhost:5000/api/ocr/test");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OCRResult>();
                    _logger.LogInformation($"OCR test successful. Confidence: {result?.Confidence}");
                    return result ?? new OCRResult { Success = false, Error = "Failed to parse test response" };
                }
                else
                {
                    _logger.LogError($"OCR test failed with status: {response.StatusCode}");
                    return new OCRResult { Success = false, Error = $"Test failed: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing Python OCR service");
                return new OCRResult { Success = false, Error = ex.Message };
            }
        }
    }
}
