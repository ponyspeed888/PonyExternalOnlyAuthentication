using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PonySpeed888Lib.Controllers
{
    [Route("GoogleOnly")]
    public class GoogleOnlyController : Controller
    {
        // GET: /<controller>/

        IConfiguration config;

        public GoogleOnlyController(IConfiguration Configuration)
        {
            config = Configuration;

        }
        [Route("Index")]

        public IActionResult Index()
        {
       
            return View();
        }


        [Authorize]

        [Route("Logout")]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect ("/");
        }



        [Authorize]
        [Route("MyDetail")]

        public IActionResult MyDetail()
        {
            return View();
        }


        [Route("Login")]

        public async Task<IActionResult> Login()
        {

            var properties = new AuthenticationProperties { RedirectUri = "/" };

            return new ChallengeResult("Google", properties);
        }

    }
}
