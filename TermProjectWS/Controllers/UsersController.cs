using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TermProjectWS.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UsersController : Controller
    {
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> FindUsersByName(string name)
        {
            return null;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IEnumerable<string> FindUsersByLocation(string city, string state)
        {
            return null;
        }

        [HttpGet]
        public IEnumerable<object> FindUsersByOrganization(string organization)
        {
            return null;
        }

        [HttpGet]
        public IEnumerable<object> GetFriends(string requestingUsername, string requestedUsername, string verificationToken)
        {
            return null;
        }

        [HttpGet]
        public object GetProfile(string requestingUsername, string requestedUsername, string verificationToken)
        {
            return null;
        }

        [HttpGet] 
        public IEnumerator<object> GetPhotos(string requestingUsername, string requestedUsername, string verificationToken)
        {
            return null;
        }

        [HttpGet]
        public IEnumerator<object> GetNewsFeed(string requestingUsername, string requestedUsername, string verificationToken)
        {
            return null;
        }
    }
}
