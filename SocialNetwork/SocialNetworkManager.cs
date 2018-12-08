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
            var user = repository.GetUser(email);
            user.Likes = repository.GetLikes(email);
            user.Dislikes = repository.GetDislikes(email);
            return user;
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

        public List<User> FindUsersByLike(string like)
        {
            return repository.FindUsersByLike(like);
        }

        public List<User> FindUsersByDislike(string dislike)
        {
            return repository.FindUsersByDislike(dislike);
        }

        public List<Friend> GetFriends(string email)
        {
            return repository.GetFriends(email);
        }

        public bool AreFriends(string userEmail1, string userEmail2)
        {
            return repository.AreFriends(userEmail1, userEmail2);
        }

        public bool AreFriends(string userEmail1, string userEmail2, Guid verificationToken)
        {
            return repository.IsFriend(userEmail1, userEmail2, verificationToken);
        }

        public List<Post> GetNewsFeed(string email)
        {
            return repository.GetNewsFeed(email);
        }

        public void UpdateLikesDislikes(string email, string[] likes, string[] dislikes)
        {
                if (repository.DeleteLikesDislikes(email))
                {
                    foreach (var like in likes)
                    {
                        repository.AddLike(email, like);
                    }

                    foreach (var dislike in dislikes)
                    {
                        repository.AddDislike(email, dislike);
                    }
                }
        }

        public Notification CreateNotification(Notification notification, string email)
        {
            return repository.CreateNotification(notification, email);
        }

        public bool ReadNotification(int notificationID)
        {
            return repository.ReadNotification(notificationID);
        }

        public Photo AddPhoto(Photo photo, string email)
        {
            return repository.AddPhoto(photo, email);
        }

        public List<Notification> GetNotifications(string email)
        {
            return repository.GetNotifications(email);
        }

        public bool CreateFriendRequest(string fromEmail, string toEmail)
        {
            return repository.CreateFriendRequest(fromEmail, toEmail);
        }

        public bool AcceptFriendRequest(string fromEmail, string toEmail)
        {
            return repository.AcceptFriendRequest(fromEmail, toEmail);
        }

        public bool DeclineFriendRequest(string fromEmail, string toEmail)
        {
            return repository.DeclineFriendRequest(fromEmail, toEmail);
        }

        public bool CancelFriendRequest(string fromEmail, string toEmail)
        {
            return repository.CancelFriendRequest(fromEmail, toEmail);
        }

        public bool RemoveFriend(string fromEmail, string toEmail)
        {
            return repository.RemoveFriend(fromEmail, toEmail);
        }

        public List<Friend> GetIncomingFriendRequests(string email)
        {
            return repository.GetIncomingFriendRequests(email);
        }

        public List<Friend> GetOutgoingFriendRequests(string email)
        {
            return repository.GetOutgoingFriendRequests(email);
        }

        public bool SetOnlineStatus(string email, bool online)
        {
            return repository.SetOnlineStatus(email, online);
        }

        public bool FollowUser(string email, string followerEmail)
        {
            return repository.FollowUser(email, followerEmail);
        }

        public bool IsFollowing(string email, string followerEmail)
        {
            return repository.IsFollowing(email, followerEmail);
        }

        public bool UnfollowUser(string email, string followerEmail)
        {
            return repository.UnfollowUser(email, followerEmail);
        }

        public Post CreatePost(Post post, string email)
        {
            return repository.CreatePost(post, email,null);
        }

        public Post CreatePost(Post post, string fromEmail, string toEmail)
        {
            return repository.CreatePost(post, fromEmail, toEmail);
        }

        public List<User> GetFollowers(string email)
        {
            return repository.GetFollowers(email);
        }
    }
}
