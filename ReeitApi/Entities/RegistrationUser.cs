﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Entities
{
    public class RegistrationUser
    {
        public Account Account { get; set; }
        public User User { get; set; }
    }
}
