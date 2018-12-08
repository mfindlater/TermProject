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

        public string GetLikes(string email)
        {
            throw new NotImplementedException();
        }

        public string GetDislikes(string email)
        {
            throw new NotImplementedException();
        }

        public List<User> FindUsersByLikes(string like)
        {
            throw new NotImplementedException();
        }

        public List<User> FindUsersByDislikes(string dislike)
        {
            throw new NotImplementedException();
        }

        public List<Friend> GetIncomingFriendRequests(string email)
        {
            throw new NotImplementedException();
        }

        public List<Friend> GetOutgoingFriendRequests(string email)
        {
            throw new NotImplementedException();
        }

        public bool ReadNotification(int notificationID)
        {
            throw new NotImplementedException();
        }

        public Photo AddPhoto(Photo photo, string email)
        {
            throw new NotImplementedException();
        }

        public PhotoAlbum CreatePhotoAlbum(PhotoAlbum photoAlbum, string email)
        {
            throw new NotImplementedException();
        }

        public bool AddPhotoToPhotoAlbum(Photo photo, PhotoAlbum photoAlbum)
        {
            throw new NotImplementedException();
        }

        public bool TagPhoto(int photoID, string email)
        {
            throw new NotImplementedException();
        }

        public Post CreatePost(Post post, string email)
        {
            throw new NotImplementedException();
        }

        public bool FollowUser(string email, string followerEmail)
        {
            throw new NotImplementedException();
        }

        public bool UnfollowUser(string email, string followerEmail)
        {
            throw new NotImplementedException();
        }

        public Chat CreateChat(string fromEmail, string toEmail)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(ChatMessage message)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessage(string email, int messageID)
        {
            throw new NotImplementedException();
        }

        public bool SetOnlineStatus(string email, bool online)
        {
            throw new NotImplementedException();
        }

        public List<User> FindUsersByLike(string like)
        {
            throw new NotImplementedException();
        }

        public List<User> FindUsersByDislike(string dislike)
        {
            throw new NotImplementedException();
        }

        public bool AreFriends(string emailA, string emailB)
        {
            throw new NotImplementedException();
        }

        public bool CancelFriendRequest(string userEmail, string requestEmail)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFriend(string userEmail, string requestEmail)
        {
            throw new NotImplementedException();
        }

        public Post CreatePost(Post post, string fromEmail, string toEmail)
        {
            throw new NotImplementedException();
        }

        public bool IsFollowing(string email, string followerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
