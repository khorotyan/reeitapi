using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Entities
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
