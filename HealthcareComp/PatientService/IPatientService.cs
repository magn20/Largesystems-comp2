using SharedModels.models;

namespace PatientService;

public interface IPatientService
{
    public Patient GetPatient(int id);
    
    public void AddPatient(Patient patient);
    
    public void DeletePatient(Patient patient);

}