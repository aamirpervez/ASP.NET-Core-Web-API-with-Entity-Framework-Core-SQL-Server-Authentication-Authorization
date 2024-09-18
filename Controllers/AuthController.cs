using ExploreAPIs.API.Modals.DTOs;
using ExploreAPIs.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExploreAPIs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTOs registerRequestDTOs)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTOs.Username,
                Email = registerRequestDTOs.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTOs.Password);


            if(identityResult.Succeeded)
            {
                //Add Roles to this Users..
                if(registerRequestDTOs.Roles != null && registerRequestDTOs.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTOs.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong!");
        }


        //Password@123 -- aamirpervez27@example.com

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTOs loginRequestDTOs)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTOs.Username);

            if(user != null)
            {
               var checkPasswordResult =  await userManager.CheckPasswordAsync(user, loginRequestDTOs.Password);

                if(checkPasswordResult)
                {
                    //Get User Roles
                   var userRoles = await userManager.GetRolesAsync(user);

                    if(userRoles != null)
                    {
                        //Create Token
                       var jwtToken = tokenRepository.CreateJWTToken(user, userRoles.ToList());

                        var response = new LoginResponseDTOs
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }

                }
            }

            return BadRequest("Username or Password incorrect!");
        }

    }
}
