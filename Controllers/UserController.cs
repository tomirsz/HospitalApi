using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HospitalApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HospitalApi.Exceptions;

namespace HospitalApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration config;
        private readonly UserService userService = UserService.Instance();

        public UserController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/signup")]
        public IActionResult Signup([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            try
            {
                User user = userService.CreateUser(login.Username, login.Password, login.FirstName, login.LastName, login.Pesel);
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    username = user.Username,
                    role = user.Role
                });
                return response;
            }
            catch (UserAlreadyExistsException e)
            {
                return response = Unauthorized(new
                {
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/signin")]
        public IActionResult Singin([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            try
            {
                User user = userService.Signup(login.Username, login.Password);
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    username = user.Username,
                    role = user.Role
                });
                return response;
            }
            catch (AuthException e)
            {
                return response = Unauthorized(new
                {
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/add/nurse")]
        public IActionResult registerNurse([FromBody] Nurse login)
        {
            IActionResult response = Unauthorized();
            try
            {
                Nurse user = userService.CreateNurse(login.Username, login.Password, login.FirstName, login.LastName, login.Pesel);
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    username = user.Username,
                    role = user.Role
                });
                return response;
            }
            catch (UserAlreadyExistsException e)
            {
                return response = Unauthorized(new
                {
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/add/doctor")]
        public IActionResult RegisterDoctor([FromBody] DoctorDTO login)
        {
            IActionResult response = Unauthorized();
            try
            {
                Doctor user = userService.CreateDoctor(login.Username, login.Password, login.FirstName, login.LastName, login.Pesel, (Specialization) Enum.Parse(typeof(Specialization), login.Specialization.ToString()), login.Pwz);
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    username = user.Username,
                    role = user.Role
                });
                return response;
            }
            catch (UserAlreadyExistsException e)
            {
                return response = Unauthorized(new
                {
                    message = e.Message
                });
            }
        }


        [HttpGet("/employees")]
        [Authorize(Policy = Policies.NURSE)]
        public IEnumerable<Nurse> FindAllNurses()
        {
            return userService.FindAllEmployees();
        }

        [HttpGet("/users")]
        [Authorize(Policy = Policies.ADMIN)]
        public IEnumerable<User> FindAllUsersForAdmin()
        {
            return userService.GetAllUsers();
        }

        [HttpGet("/user/{username}")]
        [Authorize(Policy = Policies.ADMIN)]
        public IActionResult GetUser(string username)
        {
            IActionResult response;
            try
            {
                User newUser = userService.GetUser(username);
                response = Ok(new
                {
                    user = newUser
                });
                return response;
            }
            catch (UserNotFoundException e)
            {
                return response = BadRequest(new
                {
                    message = e.Message
                });
            }

        }

        [HttpPut("/user/{username}")]
        [Authorize(Policy = Policies.ADMIN)]
        public IActionResult EditUser([FromBody] UserDTO user, string username) {
            IActionResult response;
            try
            {
                userService.EditUser(user, username);
                response = Ok(); 
                return response;
            }
            catch (UserNotFoundException e) {
                return response = BadRequest(new
                {
                    message = e.Message
                });
            }

        }

        [HttpDelete("/user/{username}")]
        [Authorize(Policy = Policies.ADMIN)]
        public IActionResult DeleteUser(string username)
        {
            IActionResult response;
            try
            {
                userService.DeleteUser(username);
                response = Ok();
                return response;
            }
            catch (UserNotFoundException e)
            {
                return response = BadRequest(new
                {
                    message = e.Message
                });
            }

        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("username", user.Username.ToString()),
            new Claim("role", user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    } 
}
