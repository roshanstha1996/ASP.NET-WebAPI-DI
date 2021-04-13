using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepo : IUserRepo
    {
        public bool Login(string username, string password)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                return entities.UserTables.Any(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}
