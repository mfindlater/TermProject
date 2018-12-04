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
        bool CreateFriendRequest(string fromEmail, string toEmail);
        bool AcceptFriendRequest(string userEmail, string requestEmail);
        bool DeclineFriendRequest(string userEmail, string requestEmail);
        bool IsFriend(string user1Email, string user2Email, Guid verificationToken);
        List<Post> GetNewsFeed(string email);
        List<Post> GetWall(string email);
    }
}
