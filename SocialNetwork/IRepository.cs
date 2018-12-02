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
        List<User> FindUsersByName(string name);
        List<User> FindUsersByLocation(string city, string state);
        List<User> FindUsersByOrganization(string orgnanization);
        List<Friend> GetFriends(string email);
    }
}
