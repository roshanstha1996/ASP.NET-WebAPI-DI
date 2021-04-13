using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository;

namespace Aastha_WebAPI_DI
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using(AasthaDBEntities entities= new AasthaDBEntities())
            {
                return entities.UserTables.Any(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}