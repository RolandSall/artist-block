using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_service.Models;


    [Table(("auth_user"))]
    public class AuthUser
    {
        [Key]
        [Column("PK_auth0_id")]
        public string Auth0Id{ get; set; }
        
        [Column("FK_auth0_registered_user_id")]
        public Guid RegisteredUserId { get; set; }
        
        public virtual RegisteredUser? RegisteredUser { get; set; }
    
}