using account_service.Service.DeploymentService;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controllers.DeploymentController;

[Route("api/v1")]
[ApiController]  
public class DeploymentController: ControllerBase
{

    private readonly IDeploymentService _service;

    public DeploymentController(IDeploymentService service)
    {
        _service = service;
    }



    [HttpPost]
    [Route("deployments")]
    public ActionResult AddDeploymentStat()
    {
        try
        {

            _service.AddDeploymentStat();
            return Ok("Sent");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}