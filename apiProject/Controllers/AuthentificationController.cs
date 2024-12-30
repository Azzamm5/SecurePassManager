using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AuthentificationController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        public AuthentificationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
    

        // [HttpPost("Register")]
        // public async Task<IActionResult> Register([FromBody] RegisterDTo registerDTo)
        // {

        // }
    }
}