using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers;

[ApiController]
[Route("api/v1/")]
public class WeatherForecastController : ControllerBase
{
  
    private readonly IConfiguration _configuration;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("app-setting-config")]   
    public ActionResult Get()
    {
        var stage = _configuration["env-version"];
        var stageEncryption = _configuration["env-encrypt"];
        var localStageVar = _configuration["Local-Property-AppSetting"];
        return Content($"Stage: {stage} \n" +
                       $"Encryption Variable Test: {stageEncryption} \n" +
                       $"Local Variable Test: {localStageVar} \n");
        
    }
}
