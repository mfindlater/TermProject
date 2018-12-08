using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using Utilities;

namespace SocialNetwork
{
    public class SqlRepository : IRepository
    {
        private readonly DBConnect db = new DBConnect();

        public bool AcceptFriendRequest(string userEmail, string requestEmail)
        {
            var command = new SqlCommand("TP_AcceptFriendRequest") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserEmail", userEmail);
            command.Parameters.AddWithValue("@RequestEmail", requestEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool CreateFriendRequest(string userEmail, string requestEmail)
        {
            var command = new SqlCommand("TP_CreateFriendRequest") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserEmail", userEmail);
            command.Parameters.AddWithValue("@RequestEmail", requestEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool CreateUser(RegisterInfo registerInfo)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var command = new SqlCommand("TP_CreateUser") { CommandType = CommandType.StoredProcedure };

                    command.Parameters.AddWithValue("@Password", EncryptionManager.Encrypt(registerInfo.Password));
                    command.Parameters.AddWithValue("@Name", registerInfo.Name);
                    command.Parameters.AddWithValue("@Email", registerInfo.ContactInfo.Email);
                    command.Parameters.AddWithValue("@Phone", registerInfo.ContactInfo.Phone);
                    command.Parameters.AddWithValue("@AddressLine1", registerInfo.Address.AddressLine1);
                    command.Parameters.AddWithValue("@AddressLine2", registerInfo.Address.AddressLine2);
                    command.Parameters.AddWithValue("@City", registerInfo.Address.City);
                    command.Parameters.AddWithValue("@State", registerInfo.Address.State);
                    command.Parameters.AddWithValue("@PostalCode", registerInfo.Address.PostalCode);
                    command.Parameters.AddWithValue("@Birthdate", registerInfo.BirthDate);

                    var settings = new UserSettings();
                    settings.SecurityQuestions = registerInfo.SecurityQuestions;

                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(ms, settings);

                    byte[] serializedSettings = ms.ToArray();

                    command.Parameters.AddWithValue("@Settings", serializedSettings);

                    int result = db.DoUpdateUsingCmdObj(command);

                    return (result != -1);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return false;
        }

        public bool DeclineFriendRequest(string userEmail, string requestEmail)
        {
            var command = new SqlCommand("TP_DeclineFriendRequest") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserEmail", userEmail);
            command.Parameters.AddWithValue("@RequestEmail", requestEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public List<User> FindUsersByLocation(string city, string state)
        {
            var users = new List<User>();

            try
            {
                var command = new SqlCommand("TP_FindUsersByLocation")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@State", state);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = GetUserFromRow(i);
                        users.Add(user);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        users[i].Photos = GetPhotos(users[i].ContactInfo.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return users;
        }

        public List<User> FindUsersByName(string name)
        {
            var users = new List<User>();

            try
            {
                var command = new SqlCommand("TP_FindUsersByName")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Name", name);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = GetUserFromRow(i);
                        users.Add(user);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        users[i].Photos = GetPhotos(users[i].ContactInfo.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return users;
        }

        public List<User> FindUsersByOrganization(string organization)
        {
            var users = new List<User>();

            try
            {
                var command = new SqlCommand("TP_FindUsersByOrganization")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Organization", organization);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = GetUserFromRow(i);
                        users.Add(user);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        users[i].Photos = GetPhotos(users[i].ContactInfo.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return users;
        }

        public List<Friend> GetFriends(string email)
        {
            var friends = new List<Friend>();

            try
            {
                var command = new SqlCommand("TP_GetFriends")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Email", email);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var friend = GetFriendFromRow(i);

                        friends.Add(friend);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return friends;
        }

        public List<Post> GetNewsFeed(string email)
        {

            var command = new SqlCommand("TP_GetNewsFeed")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            var ds = db.GetDataSetUsingCmdObj(command);

            var newsFeed = new List<Post>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var newsFeedPost = new Post()
                {
                    PostID = Convert.ToInt32(db.GetField("PostID", i)),
                    UserID = Convert.ToInt32(db.GetField("UserID", i)),
                    PostedDate = DateTime.Parse(db.GetField("PostedDate", i).ToString()),
                    Content = HttpUtility.HtmlEncode(db.GetField("Content", i))
                };
                newsFeed.Add(newsFeedPost);
            }

            return newsFeed;
        }

        public string GetPassword(string email)
        {
            try
            {
                var command = new SqlCommand("TP_GetUser")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Email", email);

                var ds = db.GetDataSetUsingCmdObj(command);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    string password = EncryptionManager.Decrypt(db.GetField("Password", 0).ToString());
                    return password;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return string.Empty;
        }

        public User GetUser(string email)
        {
            var command = new SqlCommand("TP_GetUser")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            var ds = db.GetDataSetUsingCmdObj(command);

            if (ds.Tables[0].Rows.Count > 0)
            {
                byte[] settings = (byte[])db.GetField("Settings", 0);

                using (var ms = new MemoryStream(settings))
                {
                    var binaryFormatter = new BinaryFormatter();

                    var user = GetUserFromRow(0);
                    user.Photos = GetPhotos(user.ContactInfo.Email);
                    user.Friends = GetFriends(user.ContactInfo.Email);
                    return user;
                }
            }
            return null;
        }

        public bool IsFriend(string user1Email, string user2Email, Guid verificationToken)
        {

            var command = new SqlCommand("TP_IsFriend") { CommandType = CommandType.StoredProcedure };

            command.Parameters.AddWithValue("@User1Email", user1Email);
            command.Parameters.AddWithValue("@User2Email", user2Email);
            command.Parameters.AddWithValue("@VerificationToken", verificationToken);

            var isFriendParam = new SqlParameter("@IsFriend", SqlDbType.Bit) { Direction = ParameterDirection.ReturnValue };
            command.Parameters.Add(isFriendParam);

            db.GetDataSetUsingCmdObj(command);

            bool result = Convert.ToBoolean(isFriendParam.Value);

            return result;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var command = new SqlCommand("TP_UpdateUser") { CommandType = CommandType.StoredProcedure };

                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.ContactInfo.Email);
                    command.Parameters.AddWithValue("@Phone", user.ContactInfo.Phone);
                    command.Parameters.AddWithValue("@AddressLine1", user.Address.AddressLine1);
                    command.Parameters.AddWithValue("@AddressLine2", user.Address.AddressLine2);
                    command.Parameters.AddWithValue("@City", user.Address.City);
                    command.Parameters.AddWithValue("@State", user.Address.State);
                    command.Parameters.AddWithValue("@PostalCode", user.Address.PostalCode);
                    command.Parameters.AddWithValue("@Organization", user.Organization);
                    command.Parameters.AddWithValue("@ProfilePhotoID", user.ProfilePhotoID);

                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(ms, user.Settings);

                    byte[] serializedSettings = ms.ToArray();

                    command.Parameters.AddWithValue("@Settings", serializedSettings);

                    int result = db.DoUpdateUsingCmdObj(command);

                    return (result != -1);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return false;
        }

        private List<Photo> GetPhotos(string email)
        {
            var command = new SqlCommand("TP_GetPhotos")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            var ds = db.GetDataSetUsingCmdObj(command);

            var photos = new List<Photo>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var photo = new Photo()
                {
                    PhotoID = Convert.ToInt32(db.GetField("PhotoID", i)),
                    UserID = Convert.ToInt32(db.GetField("UserID", i)),
                    URL = db.GetField("URL", 0).ToString(),
                    Description = db.GetField("Description", 0).ToString(),
                    PostedDate = DateTime.Parse(db.GetField("PostedDate", i).ToString())
                };
                photos.Add(photo);
            }

            return photos;

        }

        private User GetUserFromRow(int row)
        {
            byte[] settings = (byte[])db.GetField("Settings", row);

            var ms = new MemoryStream(settings);

            var binaryFormatter = new BinaryFormatter();
            var uSettings = (UserSettings)binaryFormatter.Deserialize(ms);

            var user = new User()
            {
                UserId = Convert.ToInt32(db.GetField("UserID", row)),
                Name = db.GetField("Name", row).ToString(),
                EncryptedPassword = db.GetField("Password", row).ToString(),
                BirthDate = DateTime.Parse(db.GetField("BirthDate", row).ToString()),
                ContactInfo = new ContactInfo()
                {
                    Email = db.GetField("Email", row).ToString(),
                    Phone = db.GetField("Phone", row).ToString()
                },
                Address = new Address()
                {
                    AddressLine1 = db.GetField("AddressLine1", row).ToString(),
                    AddressLine2 = db.GetField("AddressLine2", row).ToString(),
                    City = db.GetField("City", row).ToString(),
                    PostalCode = db.GetField("PostalCode", row).ToString(),
                    State = db.GetField("State", row).ToString()
                },
                Settings = uSettings,
            };

            user.ProfilePhotoURL = "~/img/person_temp.jpg";

            if (db.GetField("Organization", row) != DBNull.Value)
            {
                user.Organization = db.GetField("Organization", row).ToString();
            }

            if(db.GetField("ProfilePhotoID", row) != DBNull.Value)
            {
                user.ProfilePhotoID = Convert.ToInt32(db.GetField("ProfilePhotoID", row));
            }

            if (db.GetField("URL", row) != DBNull.Value)
            {
                user.ProfilePhotoURL = db.GetField("URL", row).ToString();
            }

            return user;
        }

        public List<Post> GetWall(string email)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLikesDislikes(string email)
        {
            var command = new SqlCommand("TP_DeleteLikesDislikes")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool AddLike(string email, string text)
        {
            var command = new SqlCommand("TP_AddLike")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Text", text);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool AddDislike(string email, string text)
        {
            var command = new SqlCommand("TP_AddDislike")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Text", text);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public Notification CreateNotification(Notification notification, string email)
        {
            var command = new SqlCommand("TP_CreateNotification")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@URL", notification.URL);
            command.Parameters.AddWithValue("@Description", notification.Description);

            db.GetDataSetUsingCmdObj(command);
            notification.NotificationID = Convert.ToInt32(db.GetField("NotificationID", 0));
            notification.NotificationDate = Convert.ToDateTime(db.GetField("NotificationDate", 0));
            notification.UserID = Convert.ToInt32(db.GetField("UserID", 0));

            return notification;
        }

        public List<Notification> GetNotifications(string email)
        {
            List<Notification> notifications = new List<Notification>();

            var command = new SqlCommand("TP_GetNotifications")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            var ds = db.GetDataSetUsingCmdObj(command);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Notification notification = new Notification();
                notification.NotificationID = Convert.ToInt32(db.GetField("NotificationID", i));
                notification.UserID = Convert.ToInt32(db.GetField("UserID", i));
                notification.URL = db.GetField("URL", i).ToString();
                notification.Description = db.GetField("Description", i).ToString();
                notification.ReadStatus = Convert.ToBoolean(db.GetField("ReadStatus", i));
                notification.NotificationDate = Convert.ToDateTime(db.GetField("NotificationDate", i));

                notifications.Add(notification);
            }

            return notifications;
        }

        public string GetLikes(string email)
        {
            string likes = "";

            var command = new SqlCommand("TP_GetLikes")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            var ds = db.GetDataSetUsingCmdObj(command);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                likes += db.GetField("Text", i);

                if (i != ds.Tables[0].Rows.Count - 1)
                {
                    likes += ", ";
                }
            }

            return likes;

        }

        public string GetDislikes(string email)
        {
            string dislikes = "";

            var command = new SqlCommand("TP_GetDislikes")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            var ds = db.GetDataSetUsingCmdObj(command);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dislikes += db.GetField("Text", i);

                if (i != ds.Tables[0].Rows.Count - 1)
                {
                    dislikes += ", ";
                }
            }

            return dislikes;
        }

        public bool ReadNotification(int notificationID)
        {
            var command = new SqlCommand("TP_ReadNotification")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@NotificationID", notificationID);
            int result = db.DoUpdateUsingCmdObj(command);

            return result != -1;
        }

        public Photo AddPhoto(Photo photo, string email)
        {
            var command = new SqlCommand("TP_AddPhoto")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@URL", photo.URL);
            command.Parameters.AddWithValue("@Description", photo.Description);

            db.GetDataSetUsingCmdObj(command);
            photo.PhotoID = Convert.ToInt32(db.GetField("PhotoID", 0));
            photo.PostedDate = Convert.ToDateTime(db.GetField("PostedDate", 0));

            return photo;
        }

        private string GetEmail(int userID)
        {
            var connection = db.GetConnection();

            var command = new SqlCommand("dbo.TP_GetUserID", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserID", userID);

            connection.Open();

            string email = (string)command.ExecuteScalar();

            connection.Close();

            return email;
        }

        public List<User> FindUsersByLike(string like)
        {
            var users = new List<User>();

            try
            {
                var command = new SqlCommand("TP_FindUsersByLike")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Text", like);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = GetUserFromRow(i);
                        users.Add(user);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        users[i].Photos = GetPhotos(users[i].ContactInfo.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return users;
        }

        public List<User> FindUsersByDislike(string dislike)
        {
            var users = new List<User>();

            try
            {
                var command = new SqlCommand("TP_FindUsersByDislike")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Text", dislike);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = GetUserFromRow(i);
                        users.Add(user);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        users[i].Photos = GetPhotos(users[i].ContactInfo.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return users;
        }

        public List<Friend> GetIncomingFriendRequests(string email)
        {
            var friends = new List<Friend>();

            try
            {
                var command = new SqlCommand("TP_GetIncomingFriendRequest")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Email", email);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var friend = GetFriendFromRow(i);

                        friends.Add(friend);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return friends;
        }

        public List<Friend> GetOutgoingFriendRequests(string email)
        {
            var friends = new List<Friend>();

            try
            {
                var command = new SqlCommand("TP_GetOutgoingFriendRequest")
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Email", email);

                var ds = db.GetDataSetUsingCmdObj(command);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var friend = GetFriendFromRow(i);

                        friends.Add(friend);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return friends;
        }

        public bool AddPhotoToPhotoAlbum(Photo photo, PhotoAlbum photoAlbum)
        {
            var command = new SqlCommand("TP_AddPhotoToPhotoAlbum")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PhotoAlbumID", photoAlbum.PhotoAlbumID);
            command.Parameters.AddWithValue("@PhotoID", photo.PhotoID);
            int result = db.DoUpdateUsingCmdObj(command);

            return result != -1;
        }

        public bool TagPhoto(int photoID, string email)
        {
            var command = new SqlCommand("TP_TagPhoto")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PTaggedUserEmail", email);
            command.Parameters.AddWithValue("@PhotoID", photoID);
            int result = db.DoUpdateUsingCmdObj(command);

            return result != -1;
        }

        public Post CreatePost(Post post, string fromEmail, string toEmail)
        {
            var command = new SqlCommand("TP_CreatePost")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@FromEmail", fromEmail);
            
            if(toEmail != null)
            {
                command.Parameters.AddWithValue("@FromEmail", toEmail);
            }

            if(post.Photo != null)
            {
                command.Parameters.AddWithValue("@PhotoID", post.Photo.PhotoID);
            }
          
            command.Parameters.AddWithValue("@Content", post.Content);

            var postIdParam = new SqlParameter("@PhotoAlbumID", SqlDbType.Int);
            command.Parameters.Add(postIdParam);

            int result = db.DoUpdateUsingCmdObj(command);

            post.PostID = Convert.ToInt32(postIdParam.Value);

            return post;
        }

        public bool FollowUser(string email, string followerEmail)
        {
            var command = new SqlCommand("TP_FollowUser")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@FollowerEmail",followerEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool UnfollowUser(string email, string followerEmail)
        {
            var command = new SqlCommand("TP_UnfollowUser")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@FollowerEmail", followerEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public Chat CreateChat(string fromEmail, string toEmail)
        {
            var command = new SqlCommand("TP_CreateChat")
            {
                CommandType = CommandType.StoredProcedure
            };

            var parameter = new SqlParameter("@ChatID", SqlDbType.Int);
           
            db.DoUpdateUsingCmdObj(command);

            Chat chat = new Chat
            {
                ChatID = Convert.ToInt32(parameter.Value)
            };

            return chat;
        }

        public bool SendMessage(ChatMessage message)
        {
            var command = new SqlCommand("TP_SendMessage")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ChatID", message.ChatID);
            command.Parameters.AddWithValue("@FromEmail", message.FromEmail);
            command.Parameters.AddWithValue("@ToEmail", message.ToEmail);
            command.Parameters.AddWithValue("@Message", message.Message);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool DeleteMessage(string email, int messageID)
        {
            var command = new SqlCommand("TP_DeleteMessage")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@MessageID", messageID);
            int result = db.DoUpdateUsingCmdObj(command);

            return result != -1;
        }

        public bool SetOnlineStatus(string email,bool online)
        {
            var command = new SqlCommand("TP_SetOnlineStatus")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@OnlineStatus", online);
            int result = db.DoUpdateUsingCmdObj(command);

            return result != -1;
        }

        public PhotoAlbum CreatePhotoAlbum(PhotoAlbum photoAlbum, string email)
        {
            var command = new SqlCommand("TP_CreatePhotoAlbum")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", photoAlbum.Name);
            command.Parameters.AddWithValue("@Description", photoAlbum.Description);
            command.Parameters.AddWithValue("@UserID", photoAlbum.UserID);

            var photoIdParam = new SqlParameter("@PhotoAlbumID", SqlDbType.Int);
            command.Parameters.Add(photoIdParam);

            int result = db.DoUpdateUsingCmdObj(command);

            photoAlbum.PhotoAlbumID = Convert.ToInt32(photoIdParam.Value);

            return photoAlbum;
        }

        private Friend GetFriendFromRow(int row)
        {
            var user = GetUserFromRow(row);

            var friend = new Friend()
            {
                Email = user.ContactInfo.Email,
                Name = user.Name,
                ProfilePhotoURL = user.ProfilePhotoURL,
                UserId = user.UserId,
                FriendRequestStatus = (FriendRequestStatus)Convert.ToUInt32(db.GetField("FriendRequestStatusID", row))
            };

            return friend;
        }

        public bool AreFriends(string emailA, string emailB)
        {
            var command = new SqlCommand("TP_AreFriendsSP") { CommandType = CommandType.StoredProcedure };

            command.Parameters.AddWithValue("@User1Email", emailA);
            command.Parameters.AddWithValue("@User2Email", emailB);

            var isFriendParam = new SqlParameter("@IsFriend", SqlDbType.Bit) { Direction = ParameterDirection.ReturnValue };
            command.Parameters.Add(isFriendParam);

            db.GetDataSetUsingCmdObj(command);

            bool result = Convert.ToBoolean(isFriendParam.Value);

            return result;
        }

        public bool CancelFriendRequest(string userEmail, string requestEmail)
        {
            var command = new SqlCommand("TP_CancelFriendRequest") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserEmail", userEmail);
            command.Parameters.AddWithValue("@RequestEmail", requestEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool RemoveFriend(string userEmail, string requestEmail)
        {
            var command = new SqlCommand("TP_RemoveFriend") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserEmail", userEmail);
            command.Parameters.AddWithValue("@RequestEmail", requestEmail);

            int result = db.DoUpdateUsingCmdObj(command);

            return (result != -1);
        }

        public bool IsFollowing(string email, string followerEmail)
        {
            var command = new SqlCommand("TP_IsFollowing") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@FollowerEmail", followerEmail);

            SqlParameter parameter = new SqlParameter("@Result", SqlDbType.Bit);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);

            db.GetDataSetUsingCmdObj(command);

            bool result = Convert.ToBoolean(parameter.Value);
            return result;
        }
    }
}
