

using Microsoft.AspNetCore.Mvc;
using Serilog;
using SharedModels.models;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientServiceController : ControllerBase
{

    private IPatientService _service;
    
    public PatientServiceController(IPatientService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("GetPatient/{id:int}")]
    public ActionResult GetPatient([FromRoute] int id)
    {
        Log.Logger.Debug("Entered PatientService");
        return Ok(_service.GetPatient(id));
    }
    
    [HttpPost]
    [Route("AddPatient")]
    public ActionResult AddPatient(Patient patient)
    {
        Log.Logger.Debug("Entered PatientService");
        
        _service.AddPatient(patient);
        return Ok();
    }

    [HttpDelete]
    [Route("DeletePatient")]
    public ActionResult DeletePatient(Patient patient)
    {        
        Log.Logger.Debug("Entered PatientService");

        _service.DeletePatient(patient);
        return Ok();
    }
}