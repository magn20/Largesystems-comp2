using SharedModels.models;

namespace PatientService;

public interface IPatientService
{
    public Patient GetPatient(string id);
    public List<Patient> GetAllPatient();
    
    public void AddPatient(Patient patient);
    
    public void DeletePatient(Patient patient);

}