using SharedModels.models;

namespace PatientService.Repository;

public interface IPatientRepository
{
    void RecreateDatabase();

    void AddPatient(Patient patient);

    void DeletePatient(Patient patient);

    Patient GetPatient(int ssn);
    

}