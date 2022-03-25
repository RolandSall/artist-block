using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers;

[ApiController]
[Route("api/v1/account-service")]
public class ConfigurationController : ControllerBase
{
  
    private readonly IConfiguration _configuration;

    private readonly ILogger<ConfigurationController> _logger;

    public ConfigurationController(ILogger<ConfigurationController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("hello")]   
    public ActionResult Hello()
    {
        return Ok("This Test is for Anis to be happy!");
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
