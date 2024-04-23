

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
    [Route("GetAllPatient")]
    public ActionResult GetAllPatient()
    {
        Log.Logger.Debug("Entered PatientService");
        return Ok(_service.GetAllPatient());
    }
    
    [HttpGet]
    [Route("GetPatient/{id}")]
    public ActionResult GetPatient([FromRoute] string id)
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