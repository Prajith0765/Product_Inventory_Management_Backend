using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Domain.Entities
{
    public class User
    {
        [Key]
        public long ProductUserId { get; set; }

        [Required]
        public string ProductUserName { get; set; }
        [Required]
        public string ProductUserPasswordHash { get; set; }
        [Required]
        public string ProductUserRole { get; set; }
    }
}
