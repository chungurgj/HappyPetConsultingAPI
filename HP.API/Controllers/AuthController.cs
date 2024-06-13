using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using HP.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<User> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var User = new User
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email,
                DisplayName = registerRequestDto.Name
            };

            var checkEmail = await userManager.FindByEmailAsync(User.Email);

            if(checkEmail == null)
            {
                var identityResult = await userManager.CreateAsync(User, registerRequestDto.Password);

                if (identityResult.Succeeded)
                {
                    if (registerRequestDto.Roles == null )
                    {
                        registerRequestDto.Roles = new string[] { "User" };
                    }

                        identityResult = await userManager.AddToRolesAsync(User, registerRequestDto.Roles);

                        if (identityResult.Succeeded)
                        {
                            return Ok("User was registered");
                        }
                    
                }
                return BadRequest(identityResult);
            }
            else
            {
                return BadRequest("The email is taken");
            }
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto) { 
            
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if(user!=null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPassword)
                {
                   
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var loginResponse = new LoginResponseDto
                        {
                            Id = user.Id,
                            JwtToken = jwtToken,
                            Email = loginRequestDto.Email,
                            DisplayName = user.DisplayName
                        };

                        return Ok(loginResponse);

                    }
                }
                return NotFound("The password is wrong");
            }
            return NotFound("The email is wrong");
        }

        [HttpPost]
        [Route("password-change")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequestDto changePasswordRequestDto)
        {
            var user = await userManager.FindByIdAsync(changePasswordRequestDto.userId);
            if(user==null)
            {
                return NotFound("User not found");
            }

            var validPassword = await userManager.CheckPasswordAsync(user, changePasswordRequestDto.currentPassword);

            if (!validPassword) {
                return BadRequest("Current password is not right");
            }

            var result = await userManager.ChangePasswordAsync(user, changePasswordRequestDto.currentPassword, changePasswordRequestDto.newPassword);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Password is not changed");
            }

        }

        [HttpGet]
        [Route("all-users")]
        public async Task<IActionResult> Get()
        {
            var users = await userManager.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("user-role/{userId}")]
        public async Task<IActionResult> GetUserRole([FromRoute]string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost]
        [Route("master-password-change")]
        public async Task<IActionResult> ChangePasswordMaster(string userId,string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);

            return Ok(result);

        }

        [HttpGet]
        [Route("get-vets")]
        public async Task<IActionResult> GetVets()
        {
            var vets = await userManager.GetUsersInRoleAsync("Vet");

            return Ok(vets);
        }

        [HttpGet]
        [Route("user-by-id/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute]string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return Ok(user);
        }
    }
}
