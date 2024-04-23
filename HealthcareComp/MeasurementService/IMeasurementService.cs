using SharedModels.models;

namespace MeasurementService;

public interface IMeasurementService
{
    public List<Measurement> GetPatientMeasurement(string id);

    public void AddMeasurement(Measurement measurement, string patientSsn);

    public void UpdateMeasurement(Measurement measurement);
}