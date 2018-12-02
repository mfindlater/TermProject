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
    public class UsersController : Controller
    {
        private readonly IRepository repository;
        private readonly SocialNetworkManager socialNetworkManager;
        
        public UsersController(IRepository repository)
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
            var requestedUser = socialNetworkManager.GetUser(requestedUsername);

            var friends = socialNetworkManager.GetFriends(requestedUsername);
      
            return friends;
        }

        [HttpGet("profile")]
        public User GetProfile([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            return null;
        }

        [HttpGet("photos")] 
        public IEnumerator<object> GetPhotos([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            return null;
        }

        [HttpGet("newsfeed")]
        public IEnumerator<object> GetNewsFeed([FromQuery]string requestingUsername, [FromQuery]string requestedUsername, [FromQuery]string verificationToken)
        {
            return null;
        }
    }
}
