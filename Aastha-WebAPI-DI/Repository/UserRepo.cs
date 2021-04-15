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

        public IEnumerable<UserTable> LoadAllUsers()
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                return entities.UserTables.ToList();
            }
        }

        public UserTable SaveUser(UserTable user)
        {
            using (AasthaDBEntities entities = new AasthaDBEntities())
            {
                entities.UserTables.Add(user);
                var newUserId = entities.SaveChanges();
                return entities.UserTables.FirstOrDefault(u => u.UserId == newUserId);
            }
        }
    }
}
