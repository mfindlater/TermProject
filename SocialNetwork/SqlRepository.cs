using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Utilities;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

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
            } catch(Exception ex)
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
                    for(int i=0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = GetUserFromRow(i);
                        users.Add(user);
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


                if(ds.Tables[0].Rows.Count > 0)
                {
                    string password = EncryptionManager.Decrypt(db.GetField("Password", 0).ToString());
                    return password;
                }
            }
            catch(Exception ex)
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

            if(ds.Tables[0].Rows.Count > 0)
            {
                byte[] settings = (byte[])db.GetField("Settings", 0);

                using (var ms = new MemoryStream(settings))
                {
                    var binaryFormatter = new BinaryFormatter();

                    var user = GetUserFromRow(0);
                    return user;
                }
            }
            return null;
        }

        public bool IsFriend(string user1Email, string user2Email, string verificationToken)
        {
            var command = new SqlCommand("TP_IsFriend", db.GetConnection()) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@User1Email", user1Email);
            command.Parameters.AddWithValue("@User2Email", user2Email);
            command.Parameters.AddWithValue("@VerificationToken", Guid.Parse(verificationToken));

            bool result = (bool)command.ExecuteScalar();

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
                    command.Parameters.AddWithValue("@City",  user.Address.City);
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

        private User GetUserFromRow(int row)
        {
            byte[] settings = (byte[])db.GetField("Settings", row);

            using (var ms = new MemoryStream(settings))
            {
                var binaryFormatter = new BinaryFormatter();

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
                    Settings = (UserSettings)binaryFormatter.Deserialize(ms),
                };
                return user;
            }
        }
    }
}
