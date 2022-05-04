using account_service.Models;

namespace account_service.Repository.DeploymentRepo;

public interface IDeploymentRepo
{
    void AddDeploymentStat(Deployment deployment);
    Deployment getDeploymentPerDay(string today);
    void UpdateDeploymentCounter(Deployment depl);
}