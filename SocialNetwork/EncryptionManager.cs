using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Mzsoft.BCrypt;

namespace SocialNetwork
{
    public class EncryptionManager
    { 
        private string salt = "$2a$10$W1g98wmY8D2kbaXL9SUSze";

        public EncryptionManager()
        {
        }

        public string Encrypt(string password)
        {
            return BCrypt.HashPassword(password, salt);
        }

        public bool Check(string password,string hashedPassword)
        {
            return BCrypt.CheckPassword(password, hashedPassword);
        }
    }
}
