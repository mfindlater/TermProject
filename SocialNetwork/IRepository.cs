using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public interface IRepository
    {
        List<User> GetUsers();
        User GetUser(int userId);
        User GetUser(string email);
        SecurityQuestion GetSecurityQuestion(string email);
        bool UpdateSecurityQuestions(string email,List<SecurityQuestion> questions);
        string GetPassword(string email);
        bool UpdateUser(User user);
        int CreateUser(RegisterInfo registerInfo);
    }
}
