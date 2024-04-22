

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
    public ActionResult GetPatient()
    {
        Log.Logger.Debug("Entered PatientService");
        return Ok(_service.GetPatient());
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