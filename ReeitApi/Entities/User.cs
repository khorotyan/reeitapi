using ReeitApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Username { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        [MaxLength(55)]
        public string Country { get; set; }
        [MaxLength(20)]
        public string State { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(256)]
        public string AvatarUrl { get; set; }

        public DateTime RegisterDate { get; set; }
        public string AvatarPath { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public bool IsBlocked { get; set; }
    }
}
