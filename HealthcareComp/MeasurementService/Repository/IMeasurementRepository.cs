using SharedModels.models;

namespace MeasurementService.Repository;

public interface IMeasurementRepository
{
    void RecreateDatabase();

    void AddMeasurement(Measurement measurement, int patientSsn);

    void UpdateMeasurement(Measurement measurement);

    List<Measurement> GetMeasurement(int patientSsn);
}