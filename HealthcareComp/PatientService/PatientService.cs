using System.Text.Json;
using Newtonsoft.Json;
using OpenTelemetry.Trace;
using PatientService.Repository;
using SharedModels.models;

namespace PatientService;

public class PatientService : IPatientService
{

    private IPatientRepository _repo;
    private HttpClient _client = new() { };
    private const string MeasurementApi = "measurement-api/Measurement";

    public PatientService(IPatientRepository repo)
    {
        _repo = repo;
    }
    
    public Patient GetPatient(int id)
    {
        var patient = _repo.GetPatient(id);
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"http://{MeasurementApi}/GetPatientMeasurement/{id}"));
        var resultMessage = _client.Send(request);
      
        var resultContent = resultMessage.Content.ReadAsStringAsync().Result;
        var measurements = JsonConvert.DeserializeObject<List<Measurement>>(resultContent);
        
        patient.Measurement = measurements; 
            
        return patient;
    }
    public void AddPatient(Patient patient)
    {
        _repo.AddPatient(patient);
    }
    public void DeletePatient(Patient patient)
    {
        _repo.DeletePatient(patient);
    }
}