using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SocialNetwork.Tests
{
    [TestClass]
    public class SocialNetworkTests
    {

        private FakeRepository repository;
        private SocialNetworkManager manager;

        [TestInitialize]
        public void Setup()
        {
            repository = new FakeRepository();
            manager = new SocialNetworkManager(repository);
        }

        [TestMethod]
        public void TestUserRegisterUser()
        {
            // Creating a user account. Tests password encryption.

            repository.ClearUsers();

            bool result = CreateUser1();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestUserLogin()
        {
            // Logging in a user. Tests password decryption. Check if that user is actually logged in.

            repository.ClearUsers();

            CreateUser1();

            var user = manager.LoginUser("kkhan@email.com","Password");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestPasswordEncryptionDecryption()
        {
            // Encrypting and decrypting for passwords/cookies.
            string plainTestPassword = "He5l8lo World!@%$#";
            string hashedPassword = EncryptionManager.HashPassword(plainTestPassword);

            Assert.IsTrue(EncryptionManager.CheckPassword(plainTestPassword, hashedPassword));

            string encodedPassword = EncryptionManager.Encode(plainTestPassword);

            string decodedPassword = EncryptionManager.Decode(encodedPassword);

            Assert.IsTrue(plainTestPassword == decodedPassword);
        }

        [TestMethod]
        public void TestUserLogOff()
        {
            // Logging off a user.
        }

        [TestMethod]
        public void TestUserAddFriendRequestAccepted()
        {
            // User 1 requests to add User 2 as a friend.
            // User 2 accepts a friend request from User 1.
        }

        [TestMethod]
        public void TestFriendRequestDeclined()
        {
            // User 1 requests to add User 2 as a friend.
            // User 2 declines friend request from User 1.
        }

        [TestMethod]
        public void TestPrivacySettings()
        {
            /* Scenario: 
             * User 1 is friends with User 2.
             * User 3 is friends with User 2. 
             */

            // User 1 attempts to access photos of User 2 which privacy level is set to Public (Should Succeed).
            // User 1 attempts to access photos of User 2 which is set to privacy level Friends (Should Succeed).
            // User 1 attempts to access profile information of User 3 which is set to privacy level Friends of Friends (Should Succee).
            // User 1 attempts to access photos of User 3 which are set to Only Friends (Should Fail).
        }

        [TestMethod]
        public void TestThemeSettings()
        {
            // User 1 changes some of their theme settings.
        }

        private bool CreateUser1()
        {
            var registerInfo = new RegisterInfo()
            {
                Name = "Kamala Khan",
                Password = "Password",
                BirthDate = DateTime.Now.AddYears(-18),
                ContactInfo = new ContactInfo()
                {
                    Email = "kkhan@email.com",
                    Phone = "1234567890"
                },
                SecurityQuestions = new List<SecurityQuestion>()
                {
                    new SecurityQuestion()
                    {
                        Question = "Is the sky blue?",
                        Answer = "Yes"
                    },
                }
            };

            return manager.RegisterNewUser(registerInfo);
        }

        private bool CreateUser2()
        {
            var registerInfo = new RegisterInfo()
            {
                Name = "Miles Morales",
                Password = "Password",
                BirthDate = DateTime.Now.AddYears(-18),
                ContactInfo = new ContactInfo()
                {
                    Email = "mmorales@email.com",
                    Phone = "1234567890"
                },
                SecurityQuestions = new List<SecurityQuestion>()
                {
                    new SecurityQuestion()
                    {
                        Question = "Is the sky blue?",
                        Answer = "Yes"
                    },
                }
            };

            return manager.RegisterNewUser(registerInfo);
        }

        private bool CreateUser3()
        {
            var registerInfo = new RegisterInfo()
            {
                Name = "Gwen Stacey",
                Password = "Password",
                BirthDate = DateTime.Now.AddYears(-18),
                ContactInfo = new ContactInfo()
                {
                    Email = "gstacey@email.com",
                    Phone = "1234567890"
                },
                SecurityQuestions = new List<SecurityQuestion>()
                {
                    new SecurityQuestion()
                    {
                        Question = "Is the sky blue?",
                        Answer = "Yes"
                    },
                }
            };

            return manager.RegisterNewUser(registerInfo);
        }
    }
}
