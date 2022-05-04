using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;

[Table("deployment")]
public class Deployment
{
    [Key]
    [Column("deployment_id")]
    public Guid DeploymentId{ get; set; }
    
    [Column("deployment_count")]
    public int count { get; set; } 
    
    [Column("deployment_timestamp")]
    public string timestamp { get; set; } 
    
}