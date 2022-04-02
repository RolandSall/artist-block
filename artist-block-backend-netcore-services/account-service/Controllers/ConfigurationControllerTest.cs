using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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
    
    [HttpGet]
    [Route("test")]   
    public ActionResult GetConnection()
    {
        var dbConfig = _configuration["Db-Connections:ConnectionDbString"];
        return Content(dbConfig);
    }
}
