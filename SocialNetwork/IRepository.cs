using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public interface IRepository
    {
        User GetUser(string email);
        string GetPassword(string email);
        bool UpdateUser(User user);
        bool CreateUser(RegisterInfo registerInfo);
    }
}
