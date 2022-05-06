using account_service.Models;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository.DeploymentRepo;

public class DeploymentRepo: IDeploymentRepo
{
    private readonly ArtistBlockDbContext _context;

    public DeploymentRepo(ArtistBlockDbContext context)
    {
        _context = context;
    }

    public void AddDeploymentStat(Deployment deployment)
    {
        var entity = _context.Deployments.Add(deployment).Entity;
        _context.SaveChanges();
    }

    public Deployment getDeploymentPerDay(string today)
    {
        return _context.Deployments.AsNoTracking().FirstOrDefault(d => d.timestamp.Equals(today));
    }

    public void UpdateDeploymentCounter(Deployment depl)
    {
        _context.Update(depl);
        _context.SaveChanges();
    }

    public IEnumerable<Deployment> DeploymentStatList()
    {
        return _context.Deployments.ToList();
    }
}