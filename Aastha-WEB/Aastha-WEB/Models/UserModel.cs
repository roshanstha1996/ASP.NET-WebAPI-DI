using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aastha_WEB.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}