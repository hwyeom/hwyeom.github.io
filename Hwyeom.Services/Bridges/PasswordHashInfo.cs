﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hwyeom.Services.Bridges
{
    public class PasswordHashInfo
    {
        public string GUIDSalt { get; set; }

        public string RNGSalt { get; set; }

        public string PasswordHash { get; set; }
    }
}
