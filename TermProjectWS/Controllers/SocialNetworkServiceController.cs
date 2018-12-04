using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork;

namespace TermProjectWS.Controllers
{
    [Produces("application/json")]
    [Route("api/SocialNetworkService")]
    public class SocialNetworkServiceController : Controller
    {
        private readonly IRepository repository;
        private readonly SocialNetworkManager socialNetworkManager;
        
        public SocialNetworkServiceController(IRepository repository)
        {
            this.repository = repository;
            socialNetworkManager = new SocialNetworkManager(repository);
        }
        
        [HttpGet("searchByName/{name}")]
        public List<User> FindUsersByName(string name)
        {
            return socialNetworkManager.FindUsersByName(name);
        }
        
        [HttpGet("searchByLocation/{city}/{state}")]
        public List<User> FindUsersByLocation(string city, string state)
        {
            return socialNetworkManager.FindUsersByLocation(city, state);
        }

        [HttpGet("searchByOrganization/{organization}")]
        public List<User> FindUsersByOrganization(string organization)
        {
            return socialNetworkManager.FindUsersByOrganization(organization); ;
        }
        
        [HttpGet("friends")]
        public List<Friend> GetFriends([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            var friends = new List<Friend>();

            if(socialNetworkManager.AreFriends(requestingUsername, requestedUsername, Guid.Parse(verificationToken)))
            {
                friends = socialNetworkManager.GetFriends(requestedUsername);
            }

            return friends;
        }

        [HttpGet("profile")]
        public User GetProfile([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            if (socialNetworkManager.AreFriends(requestingUsername, requestedUsername, Guid.Parse(verificationToken)))
            {
                return socialNetworkManager.GetUser(requestedUsername);
            }

            return null; 
        }

        [HttpGet("photos")] 
        public List<Photo> GetPhotos([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            var photos = new List<Photo>();

            if (socialNetworkManager.AreFriends(requestingUsername, requestedUsername, Guid.Parse(verificationToken)))
            {
                photos = socialNetworkManager.GetUser(requestedUsername).Photos;
            }

            return photos;
        }

        [HttpGet("newsfeed")]
        public List<Post> GetNewsFeed([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            var newsFeed = new List<Post>();

            if (socialNetworkManager.AreFriends(requestingUsername, requestedUsername, Guid.Parse(verificationToken)))
            {
                newsFeed = socialNetworkManager.GetNewsFeed(requestedUsername);
            }

            return newsFeed;
        }
    }
}
