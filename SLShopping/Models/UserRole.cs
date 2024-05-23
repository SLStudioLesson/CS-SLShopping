using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SLShopping.Models
{
    [Keyless]
    public class UserRole
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
