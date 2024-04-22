﻿using MeasurementService.Repository;
using SharedModels.models;

namespace MeasurementService;

public class MeasurementService : IMeasurementService
{

    private IMeasurementRepository _Repository { get; set; }
    
    public MeasurementService(IMeasurementRepository repository)
    {
        _Repository = repository;
    }
    
    public List<Measurement> GetPatientMeasurement(int id)
    {
        return _Repository.GetMeasurement(id);
    }

    public void AddMeasurement(Measurement measurement, int patientSsn)
    {
        _Repository.AddMeasurement(measurement, patientSsn);
    }

    public void UpdateMeasurement(Measurement measurement)
    {
        _Repository.UpdateMeasurement(measurement);
    }
}