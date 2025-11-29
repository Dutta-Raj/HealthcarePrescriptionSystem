from flask import Flask, request, jsonify
from flask_cors import CORS
import logging
import time

# Setup logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

app = Flask(__name__)
CORS(app)

class PrescriptionOCR:
    def __init__(self):
        logger.info("📄 Prescription OCR Model Initialized")
    
    def predict(self, image_data):
        """
        This is where your actual CNN + LSTM + BiLSTM model will go
        For now, using mock data
        """
        # Simulate processing time
        time.sleep(0.5)
        
        return {
            'extracted_text': 'Dr. Sharma\nPatient: Raj Kumar\n\nMedications:\n- Paracetamol 500mg: 1 tab twice daily for 3 days\n- Amoxicillin 250mg: 1 cap three times daily for 5 days',
            'confidence': 0.91,
            'medications': [
                {
                    'drug_name': 'Paracetamol',
                    'dosage': '500mg',
                    'frequency': 'twice daily',
                    'duration': '3 days'
                },
                {
                    'drug_name': 'Amoxicillin', 
                    'dosage': '250mg',
                    'frequency': 'three times daily',
                    'duration': '5 days'
                }
            ]
        }

# Initialize OCR model
ocr_model = PrescriptionOCR()

@app.route('/api/health', methods=['GET'])
def health_check():
    return jsonify({
        "status": "✅ OCR Service Running", 
        "version": "1.0",
        "model": "CNN + LSTM + BiLSTM (Mock)"
    })

@app.route('/api/ocr/prescription', methods=['POST'])
def process_prescription():
    try:
        if 'image' not in request.files:
            return jsonify({"success": False, "error": "No image file"}), 400
        
        file = request.files['image']
        logger.info(f"Processing: {file.filename}")
        
        # Read image data
        image_data = file.read()
        
        if len(image_data) == 0:
            return jsonify({"success": False, "error": "Empty image file"}), 400
        
        # Process with OCR model
        result = ocr_model.predict(image_data)
        
        return jsonify({
            "success": True,
            "text": result['extracted_text'],
            "confidence": result['confidence'],
            "medications": result['medications'],
            "message": "OCR processing completed successfully"
        })
        
    except Exception as e:
        logger.error(f"Error: {str(e)}")
        return jsonify({"success": False, "error": str(e)}), 500

@app.route('/api/ocr/test', methods=['GET'])
def test_ocr():
    """Test endpoint that returns mock data without image upload"""
    result = ocr_model.predict(None)
    return jsonify({
        "success": True,
        "text": result['extracted_text'],
        "confidence": result['confidence'],
        "medications": result['medications'],
        "message": "Test data from OCR model"
    })

if __name__ == '__main__':
    logger.info("🚀 Starting Healthcare OCR Service on port 5000")
    app.run(host='0.0.0.0', port=5000, debug=True)
