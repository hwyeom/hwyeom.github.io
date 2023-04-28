using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hwyeom.Data.DataModels
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string GUIDSalt { get; set; }
        public string RNGSalt { get; set; }
        public string PasswordHash { get; set; }

        public int AccessFailedCount { get; set; }
        public bool IsMembershipWithdrawn { get; set; }
        public System.DateTime JoinDateUTC { get; set; }

    }
}
