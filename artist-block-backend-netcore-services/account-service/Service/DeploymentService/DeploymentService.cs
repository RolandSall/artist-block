using account_service.Models;
using account_service.Repository.DeploymentRepo;

namespace account_service.Service.DeploymentService;

public class DeploymentService: IDeploymentService
{
    private readonly IDeploymentRepo _repo;

    public DeploymentService(IDeploymentRepo repo)
    {
        _repo = repo;
    }

    public void AddDeploymentStat()
    {

       var depl =  _repo.getDeploymentPerDay(DateTime.Today.ToShortDateString());
        
       if(depl == null){ 
        var Deployment = new Deployment()
        {
            DeploymentId = Guid.NewGuid(),
            timestamp = DateTime.Today.ToShortDateString(),
            count = 1
        };
        
        _repo.AddDeploymentStat(Deployment);
       }

       depl.count += 1;
       _repo.UpdateDeploymentCounter(depl);

    }
}