using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Entities
{
    public class UserToken
    {
        public User User { get; set; }
        public TokenInfo TokenInfo { get; set; }
    }
}
