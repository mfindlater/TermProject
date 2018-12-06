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
            return FindUsersByLocationTwo(city, state);
            /*var users = new List<User>();

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

            return users;*/
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
                        var user = GetUserFromRow(i);

                        var friend = new Friend()
                        {
                            Name = user.Name,
                            ProfilePhotoURL = user.ProfilePhotoURL,
                            UserId = user.UserId
                        };

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

        public List<User> FindUsersByLocationTwo(string city, string state)
        {
            var users = new List<User>();

            try
            {
                var connection = db.GetConnection();
                var command = new SqlCommand("TP_FindUsersByLocation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@State", state);
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var user = GetUserFromRow(reader);
                    users.Add(user);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return users;
        }

        private User GetUserFromRow(SqlDataReader reader)
        {

            //byte[] settings = (byte[])reader.GetValue(12);

            //var ms = new MemoryStream(settings);
            var stream = reader.GetStream(12);

            var binaryFormatter = new BinaryFormatter();
            long count = stream.Length;
            var uSettings = (UserSettings)binaryFormatter.Deserialize(stream);

            var user = new User()
            {
                UserId = Convert.ToInt32(reader["UserID"]),
                Name = reader["Name"].ToString(),
                EncryptedPassword = reader["Password"].ToString(),
                BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                ContactInfo = new ContactInfo()
                {
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                },
                Address = new Address()
                {
                    AddressLine1 = reader["AddressLine1"].ToString(),
                    AddressLine2 = reader["AddressLine2"].ToString(),
                    City = reader["City"].ToString(),
                    PostalCode = reader["PostalCode"].ToString(),
                    State = reader["State"].ToString()
                },
                Organization = reader["Organization"].ToString(),
                ProfilePhotoURL = reader["URL"].ToString(),
                Settings = uSettings,
            };
            return user;
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
                Organization = db.GetField("Organization", row).ToString(),
                ProfilePhotoURL = db.GetField("URL", row).ToString(),
                Settings = uSettings,
            };

            if (db.GetField("Organization", row) != null)
            {
                user.Organization = db.GetField("Organization", row).ToString();
            }
            if (db.GetField("URL", row) != null)
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
    }
}
