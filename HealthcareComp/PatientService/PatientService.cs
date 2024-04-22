using PatientService.Repository;
using SharedModels.models;

namespace PatientService;

public class PatientService : IPatientService
{

    private IPatientRepository _repo;
    
    public PatientService(IPatientRepository repo)
    {
        _repo = repo;
    }
    
    public List<Patient> GetPatient()
    {
        return _repo.GetPatient();
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