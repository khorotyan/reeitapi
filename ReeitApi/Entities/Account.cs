using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Entities
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }
        [Required]
        [MaxLength(128)]
        public string Password { get; set; }
    }
}
