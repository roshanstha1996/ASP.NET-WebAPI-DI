using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepo
    {
        bool Login(string username, string password);
        IEnumerable<UserTable> LoadAllUsers();

        UserTable SaveUser(UserTable user);
    }
}
