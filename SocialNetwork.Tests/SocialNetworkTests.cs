using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SocialNetwork.Tests
{
    [TestClass]
    public class SocialNetworkTests
    {
        [TestMethod]
        public void TestUserRegisterUser()
        {
            // Creating a user account. Tests password encryption.
        }

        [TestMethod]
        public void TestUserLogin()
        {
            // Logging in a user. Tests password decryption. Check if that user is actually logged in.
        }

        [TestMethod]
        public void TestPasswordEncryptionDecryption()
        {
            // Encrypting and decrypting password for cookies.
            // NOTE: this is different from encrypting/decrypting for login.
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
    }
}
