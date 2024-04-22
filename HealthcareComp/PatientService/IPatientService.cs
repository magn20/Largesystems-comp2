using SharedModels.models;

namespace PatientService;

public interface IPatientService
{
    public List<Patient> GetPatient();
    
    public void AddPatient(Patient patient);
    
    public void DeletePatient(Patient patient);

}