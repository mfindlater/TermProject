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

        public bool RegisterNewUser(RegisterInfo registerInfo)
        {
            return repository.CreateUser(registerInfo);
        }

        public User LoginUser(string email, string password)
        {
            User user = repository.GetUser(email);

            if(EncryptionManager.Decrypt(user.EncryptedPassword) == password)
            {
                return user;
            }

            return null;
        }

        public string GetUserPassword(string email)
        {
            return repository.GetPassword(email);
        }

        public User Login(string email, string password)
        {
            if(repository.GetPassword(email) == password)
            {
                return repository.GetUser(email);
            }

            return null;
        }

        public bool UpdateUser(User user)
        {
            return repository.UpdateUser(user);
        }

        public User GetUser(string email)
        {
            return repository.GetUser(email);
        }

        public SecurityQuestion GetRandomQuestion(string email)
        {
            SecurityQuestion securityQuestion = new SecurityQuestion();
            User user = repository.GetUser(email);
            Random random = new Random();
            int index = random.Next(0, user.Settings.SecurityQuestions.Count);
            securityQuestion.Question = user.Settings.SecurityQuestions[index].Question;
            securityQuestion.Answer = user.Settings.SecurityQuestions[index].Answer;

            return securityQuestion;
        }

        public List<User> FindUsersByName(string name)
        {
            return repository.FindUsersByName(name);
        }

        public List<User> FindUsersByLocation(string city, string state)
        {
            return repository.FindUsersByLocation(city, state);
        }

        public List<User> FindUsersByOrganization(string organization)
        {
            return repository.FindUsersByOrganization(organization);
        }

        public List<Friend> GetFriends(string email)
        {
            return repository.GetFriends(email);
        }

        public bool AreFriends(string userEmail1, string userEmail2, Guid verificationToken)
        {
            return repository.IsFriend(userEmail1, userEmail2, verificationToken);
        }
    }
}
