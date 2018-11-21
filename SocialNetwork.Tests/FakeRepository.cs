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
                }
            };

            user.EncryptedPassword = EncryptionManager.Encode(registerInfo.Password);

            users.Add(user);

            return true;
        }

        public string GetPassword(string email)
        {
           var user = users.Where(u => u.ContactInfo.Email == email).First();
            return EncryptionManager.Decode(user.EncryptedPassword);
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
    }
}
