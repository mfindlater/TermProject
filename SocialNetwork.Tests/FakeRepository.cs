using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork;

namespace SocialNetwork.Tests
{
    public class FakeRepository : IRepository
    {
        private readonly List<User> users = new List<User>();
        private readonly Dictionary<string, List<Friend>> friendDict = new Dictionary<string, List<Friend>>();
        private static int nextId = 0;

        public bool CreateUser(RegisterInfo registerInfo)
        {
            nextId++;

            var user = new User()
            {
                UserId = nextId,
                Address = registerInfo.Address,
                Name = registerInfo.Name,
                BirthDate = registerInfo.BirthDate,
                ContactInfo = registerInfo.ContactInfo,
                Settings = new UserSettings()
                {
                  SecurityQuestions = registerInfo.SecurityQuestions
                },
                Organization = registerInfo.Organization,
            };

            user.EncryptedPassword = EncryptionManager.Encrypt(registerInfo.Password);

            users.Add(user);

            return true;
        }

        public string GetPassword(string email)
        {
           var user = users.Where(u => u.ContactInfo.Email == email).First();
            return EncryptionManager.Decrypt(user.EncryptedPassword);
        }

        public SecurityQuestion GetSecurityQuestion(string email)
        {
            var user = users.Where(u => u.ContactInfo.Email == email).First();

            Random random = new Random();

            int randomIndex = random.Next(0, user.Settings.SecurityQuestions.Count);

            var securityQuestion = user.Settings.SecurityQuestions[randomIndex];

            return securityQuestion;
        }

        public User GetUser(int userId)
        {
            var user = users.Where(u => u.UserId == userId).First();
            return user;
        }

        public User GetUser(string email)
        {
            var user = users.Where(u => u.ContactInfo.Email == email).First();
            return user;
        }

        public bool UpdateSecurityQuestions(string email,List<SecurityQuestion> questions)
        {
            var user = users.Where(u => u.ContactInfo.Email == email).First();

            user.Settings.SecurityQuestions = questions;

            return true;
        }

        public bool UpdateUser(User user)
        {
            var updatedUser = users.Where(u => u.ContactInfo.Email == user.ContactInfo.Email).First();
            updatedUser = user;
            return true;
        }

        public void ClearUsers()
        {
            users.Clear();
        }

        public List<User> FindUsersByName(string name)
        {
            return users.Where(u => u.Name.Contains(name)).ToList();
        }

        public List<User> FindUsersByLocation(string city, string state)
        {
            return users.Where(u => u.Address.City == city && u.Address.State == state).ToList();
        }

        public List<User> FindUsersByOrganization(string orgnanization)
        {
            return users.Where(u => u.Organization.Contains(orgnanization)).ToList();
        }

        public List<Friend> GetFriends(string email)
        {
            return friendDict[email];
        }

        public bool CreateFriendRequest(string fromEmail, string toEmail)
        {
            throw new NotImplementedException();
        }

        public bool AcceptFriendRequest(string userEmail, string requestEmail)
        {
            throw new NotImplementedException();
        }

        public bool DeclineFriendRequest(string userEmail, string requestEmail)
        {
            throw new NotImplementedException();
        }

        public bool IsFriend(string user1Email, string user2Email, string verificationToken)
        {
            throw new NotImplementedException();
        }

        public bool IsFriend(string user1Email, string user2Email, Guid verificationToken)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetNewsFeed(string email)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetWall(string email)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLikesDislikes(string email)
        {
            throw new NotImplementedException();
        }

        public bool AddLike(string email, string text)
        {
            throw new NotImplementedException();
        }

        public bool AddDislike(string email, string text)
        {
            throw new NotImplementedException();
        }

        public Notification CreateNotification(Notification notification, string email)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetNotifications(string email)
        {
            throw new NotImplementedException();
        }
    }
}
