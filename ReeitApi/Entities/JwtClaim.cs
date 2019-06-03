using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Entities
{
    public class JwtClaim
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
