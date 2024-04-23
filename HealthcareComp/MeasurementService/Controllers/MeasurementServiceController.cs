using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SharedModels.models;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementServiceController : ControllerBase
{
    private IMeasurementService _measurementService;
    
    public MeasurementServiceController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }
     [HttpGet]
    [Route("GetPatientMeasurement/{id}")]
    public async Task<ActionResult<List<Measurement>>> GetPatientMeasurement([FromRoute] string id)
    {
        Log.Logger.Debug("Entered Measurement Api");
        
        return Ok(_measurementService.GetPatientMeasurement(id));
    }
    
    [HttpPost]
    [Route("AddMeasurement/{patientSsn}")]
    public async Task<ActionResult> AddMeasurement(Measurement measurement, [FromRoute] string patientSsn)
    {
        Log.Logger.Debug("Entered Measurement controller");
        _measurementService.AddMeasurement(measurement, patientSsn);
        return Ok("");
    }
    
    [HttpPut]
    [Route("UpdateMeasurement")]
    public async Task<ActionResult> UpdateMeasurement(Measurement measurement)
    {
        Log.Logger.Debug("Entered Measurement controller");
        _measurementService.UpdateMeasurement(measurement);
        return Ok("");
    }
    
}