﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork
{
    public interface IRepository
    {
        User GetUser(string email);
        string GetPassword(string email);
        bool UpdateUser(User user);
        bool CreateUser(RegisterInfo registerInfo);
        List<User> FindUsersByName(string name);
        List<User> FindUsersByLocation(string city, string state);
        List<User> FindUsersByOrganization(string orgnanization);
        List<User> FindUsersByLike(string like);
        List<User> FindUsersByDislike(string dislike);
        List<Friend> GetFriends(string email);
        List<Friend> GetIncomingFriendRequests(string email);
        List<Friend> GetOutgoingFriendRequests(string email);
        bool CreateFriendRequest(string fromEmail, string toEmail);
        bool AcceptFriendRequest(string userEmail, string requestEmail);
        bool DeclineFriendRequest(string userEmail, string requestEmail);
        bool CancelFriendRequest(string userEmail, string requestEmail);
        bool RemoveFriend(string userEmail, string requestEmail);
        bool IsFriend(string user1Email, string user2Email, Guid verificationToken);
        bool IsFriendOfFriend(string emailA, string emailB);
        bool AreFriends(string emailA, string emailB);
        List<Post> GetNewsFeed(string email);
        List<Post> GetWall(string email);
        bool DeleteLikesDislikes(string email);
        bool AddLike(string email, string text);
        bool AddDislike(string email, string text);
        string GetLikes(string email);
        string GetDislikes(string email);
        Notification CreateNotification(Notification notification, string email);
        List<Notification> GetNotifications(string email);
        bool ReadNotification(int notificationID);
        Photo AddPhoto(Photo photo, string email);
        PhotoAlbum CreatePhotoAlbum(PhotoAlbum photoAlbum, string email);
        bool AddPhotoToPhotoAlbum(Photo photo, PhotoAlbum photoAlbum);
        bool TagPhoto(int photoID, string email);
        Post CreatePost(Post post, string fromEmail, string toEmail);
        bool FollowUser(string email, string followerEmail);
        bool UnfollowUser(string email, string followerEmail);
        bool IsFollowing(string email, string followerEmail);
        List<User> GetFollowers(string email);
        List<Theme> GetThemes();
        bool SendMessage(ChatMessage message);
        bool DeleteMessage(string email,int messageID);
        bool SetOnlineStatus(string email,bool online);
        List<ChatMessage> GetMessages(string email1, string email2);
        bool DeletePhoto(int photoID);
    }
}
