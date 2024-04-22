using SharedModels.models;

namespace MeasurementService;

public interface IMeasurementService
{
    public List<Measurement> GetPatientMeasurement(int id);

    public void AddMeasurement(Measurement measurement, int patientSsn);

    public void UpdateMeasurement(Measurement measurement);
}