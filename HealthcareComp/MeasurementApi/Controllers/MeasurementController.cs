using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using Serilog;
using SharedModels.models;

namespace MeasurementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private HttpClient _client = new() { };
    private const string MeasurementService = "measurement-service/MeasurementService";

    [HttpGet]
    [Route("GetPatientMeasurement/{id}")]
    public async Task<ActionResult<List<Measurement>>> GetPatientMeasurement([FromRoute] string id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"http://{MeasurementService}/GetPatientMeasurement/{id}"));
        Log.Logger.Debug("Entered Measurement Api");

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
    [Route("AddMeasurement/{patientSsn}")]
    public async Task<ActionResult> AddMeasurement(Measurement measurement, [FromRoute] string patientSsn)
    {
        Log.Logger.Debug("Entered Measurement controller");

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"http://{MeasurementService}/AddMeasurement/{patientSsn}"));
        request.Content = new StringContent(JsonSerializer.Serialize(measurement), System.Text.Encoding.UTF8,
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
    
    [HttpPut]
    [Route("UpdateMeasurement")]
    public async Task<ActionResult> UpdateMeasurement(Measurement measurement)
    {
        Log.Logger.Debug("Entered Measurement controller");

        var request = new HttpRequestMessage(HttpMethod.Put, new Uri($"http://{MeasurementService}/UpdateMeasurement"));
        request.Content = new StringContent(JsonSerializer.Serialize(measurement), System.Text.Encoding.UTF8,
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