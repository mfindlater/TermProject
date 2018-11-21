using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public class SocialNetworkManager
    {
        private readonly IRepository repository;

        public SocialNetworkManager(IRepository repository)
        {
            this.repository = repository;
        }

        public int RegisterNewUser(RegisterInfo registerInfo)
        {
            return repository.CreateUser(registerInfo);
        }

        public User LoginUser(string email, string password)
        {
            User user = repository.GetUser(email);

            if(EncryptionManager.Decode(user.EncryptedPassword) == password)
            {
                return user;
            }

            return null;
        }

        public SecurityQuestion GetUserSecurityQuestion(string email)
        {
            return repository.GetSecurityQuestion(email);
        }

        public bool UpdateSecurityQuestions(string email,List<SecurityQuestion> questions)
        {
            return repository.UpdateSecurityQuestions(email,questions);
        }

        public string GetUserPassword(string email)
        {
            return repository.GetPassword(email);
        }

        public bool UpdateUser(User user)
        {
            return repository.UpdateUser(user);
        }
    }
}
