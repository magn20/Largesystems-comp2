using SharedModels.models;

namespace MeasurementService.Repository;

public interface IMeasurementRepository
{
    void RecreateDatabase();

    void AddMeasurement(Measurement measurement, string patientSsn);

    void UpdateMeasurement(Measurement measurement);

    List<Measurement> GetMeasurement(string patientSsn);
}