using System.Net;
using System.Text.Json;
using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using Serilog;
using SharedModels.models;



namespace PatientApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private HttpClient _client = new() { };

    private const string PatientService = "patient-service/PatientService";

    private EdgeFeatureHubConfig _edgeFeatureHubConfig;
    
    public PatientController(EdgeFeatureHubConfig edgeFeatureHubConfig)
    {
        _edgeFeatureHubConfig = edgeFeatureHubConfig;
    }
    
    [HttpGet]
    [Route("GetAllPatient")]
    public async Task<ActionResult> GetAllPatient()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"http://{PatientService}/GetAllPatient"));
        Log.Logger.Debug("Entered Patient controller");

        TraceRequest.InjectContext(request);

        var resultMessage = await _client.SendAsync(request);

        if (resultMessage.IsSuccessStatusCode)
        {
            var resultContent = await resultMessage.Content.ReadAsStringAsync();
            return Ok(resultContent);
        }

        if (resultMessage.StatusCode == HttpStatusCode.InternalServerError)
        {
            Log.Logger.Error("failed with status code of {resultMessageStatusCode} message is: {resultMessage}",
                resultMessage.StatusCode, resultMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read database");
        }

        Log.Logger.Error(
            "unknown issue occured with status code of {resultMessageStatusCode} message is: {resultMessage}",
            resultMessage.StatusCode, resultMessage);
        return Problem("Unknown error occured");

    }
    
    
    
    [HttpGet]
    [Route("GetPatient/{id}")]
    public async Task<ActionResult> GetPatient([FromRoute] string id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"http://{PatientService}/GetPatient/{id}"));
        Log.Logger.Debug("Entered Patient controller");

        TraceRequest.InjectContext(request);

        var resultMessage = await _client.SendAsync(request);

        if (resultMessage.IsSuccessStatusCode)
        {
            var resultContent = await resultMessage.Content.ReadAsStringAsync();
            return Ok(resultContent);
        }

        if (resultMessage.StatusCode == HttpStatusCode.InternalServerError)
        {
            Log.Logger.Error("failed with status code of {resultMessageStatusCode} message is: {resultMessage}",
                resultMessage.StatusCode, resultMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read database");
        }

        Log.Logger.Error(
            "unknown issue occured with status code of {resultMessageStatusCode} message is: {resultMessage}",
            resultMessage.StatusCode, resultMessage);
        return Problem("Unknown error occured");

    }
    
    [HttpPost]
    [Route("AddPatient")]
    public async Task<ActionResult> AddPatient(Patient patient)
    {
        Log.Logger.Debug("Entered Patient controller");

        var fh = await _edgeFeatureHubConfig.NewContext().Build();
        if (fh["AddPatient"].IsEnabled)
        {
            Log.Logger.Information("AddPatient is disabled");
            return BadRequest("Feature disabled");
        }
        
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"http://{PatientService}/AddPatient"));
        request.Content = new StringContent(JsonSerializer.Serialize(patient), System.Text.Encoding.UTF8,
            "application/json");

        TraceRequest.InjectContext(request);

        
        var resultMessage = await _client.SendAsync(request);
        if (resultMessage.IsSuccessStatusCode)
        {
            string resultContent = await resultMessage.Content.ReadAsStringAsync();
            return Ok(resultContent);
        }

        Log.Logger.Error("failed with status code of {resultMessageStatusCode} message is: {resultMessage}",
            resultMessage.StatusCode, resultMessage);
        return BadRequest($"failed with status code of {resultMessage.StatusCode} message is: {resultMessage}");
    }

    [HttpDelete]
    [Route("DeletePatient")]
    public async Task<ActionResult> DeletePatient(Patient patient)
    {
        Log.Logger.Debug("Entered Patient controller");
        
        var fh = await _edgeFeatureHubConfig.NewContext().Build();
        if (fh["DeletePatient"].IsEnabled)
        {
            Log.Logger.Information("DeletePatient is disabled");
            return BadRequest("Feature disabled");
        }
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri($"http://{PatientService}/DeletePatient"));
        request.Content = new StringContent(JsonSerializer.Serialize(patient), System.Text.Encoding.UTF8,
            "application/json");

        TraceRequest.InjectContext(request);
        
        var resultMessage = await _client.SendAsync(request);
        if (resultMessage.IsSuccessStatusCode)
        {
            string resultContent = await resultMessage.Content.ReadAsStringAsync();
            return Ok(resultContent);
        }

        Log.Logger.Error("failed with status code of {resultMessageStatusCode} message is: {resultMessage}",
            resultMessage.StatusCode, resultMessage);
        return BadRequest($"failed with status code of {resultMessage.StatusCode} message is: {resultMessage}");
    }
}