using account_service.Models;

namespace account_service.Service.DeploymentService;

public interface IDeploymentService
{
    void AddDeploymentStat();
    IEnumerable<Deployment> DeploymentStatList();
}