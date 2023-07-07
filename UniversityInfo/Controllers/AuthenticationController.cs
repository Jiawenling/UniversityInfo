using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityInfo.Models;
using UniversityInfo.Service;

namespace UniversityInfo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController: ControllerBase
    {
        private readonly UserManager<User> _userRepo;
        private readonly AccessTokenGenerator _tokenGenerator;

        public AuthenticationController(UserManager<User> userRepository, AccessTokenGenerator tokenGenerator)
        {
            _userRepo = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Login registerRequest)
        {
            User newUser = new User()
            {
                UserName = registerRequest.Username,
            };
            IdentityResult result = await _userRepo.CreateAsync(newUser, registerRequest.Password);    ;
            if(!result.Succeeded){
                IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
                IdentityError error = result.Errors.FirstOrDefault();
                if(error.Code == nameof(errorDescriber.DuplicateUserName)) return Conflict("Username already exists");
            }
            return Ok($"User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            User user = await _userRepo.FindByNameAsync(login.Username);
            if(user==null) return Unauthorized("Username and/or password is incorrect.");
            Console.WriteLine($"{user.UserName}, {user.PasswordHash}");
            bool isCorrectPassword = await _userRepo.CheckPasswordAsync(user, login.Password);
            Console.WriteLine(isCorrectPassword);
            if (!isCorrectPassword) return Unauthorized();
            string accessToken = _tokenGenerator.GenerateToken(user);
            return Ok(accessToken);
        }

    }
}