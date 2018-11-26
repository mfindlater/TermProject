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
       

        public bool CreateUser(RegisterInfo registerInfo)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var command = new SqlCommand("TP_CreateUser") { CommandType = CommandType.StoredProcedure };

                    command.Parameters.AddWithValue("@Password", EncryptionManager.Encode(registerInfo.Password));
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
                    string password = EncryptionManager.Decode(db.GetField("Password", 0).ToString());
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

                    var user = new User()
                    {
                        UserId = Convert.ToInt32(db.GetField("UserID", 0)),
                        Name = db.GetField("Name", 0).ToString(),
                        EncryptedPassword = db.GetField("Password", 0).ToString(),
                        BirthDate = DateTime.Parse(db.GetField("BirthDate", 0).ToString()),
                        ContactInfo = new ContactInfo()
                        {
                            Email = db.GetField("Email", 0).ToString(),
                            Phone = db.GetField("Phone", 0).ToString()
                        },
                        Address = new Address()
                        {
                            AddressLine1 = db.GetField("AddressLine1", 0).ToString(),
                            AddressLine2 = db.GetField("AddressLine2", 0).ToString(),
                            City = db.GetField("City", 0).ToString(),
                            PostalCode = db.GetField("PostalCode", 0).ToString(),
                            State = db.GetField("State", 0).ToString()
                        },
                        ProfilePhotoURL = db.GetField("URL", 0).ToString(),
                        Settings = (UserSettings)binaryFormatter.Deserialize(ms),
                    };
                    return user;
                }
            }
            return null;
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

                    var settings = new UserSettings();

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
    }
}
