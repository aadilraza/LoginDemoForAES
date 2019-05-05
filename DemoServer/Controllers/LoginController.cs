using DemoServer;
using DemoServer.Models;
using DemoServer.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace olympic.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("api/Login")]
        public HttpResponseMessage Login(User user)
        {
            User u = new UserRepository().GetUser(user.Username);
            if (u == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                     "The user was not found.");
            }
                
            bool credentials = u.Password.Equals(user.Password);

            if (!credentials)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                "The username/password combination was wrong.");
            }
            else
            { 
            return Request.CreateResponse(HttpStatusCode.OK,
                 TokenManager.GenerateToken(user.Username));
            }
        }
    }
}
