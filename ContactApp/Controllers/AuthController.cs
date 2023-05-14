using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ContactApp.Db;
using Microsoft.EntityFrameworkCore;
using ContactApp.Services;

namespace ContactApp.Controllers
{
    // A controller which holds methods for authentication, authorization and user control
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ContactAppDbContext _context;
        
        private readonly IConfiguration _configuration;

        private readonly IContactService _contactService;

        // Constructor
        public AuthController(ContactAppDbContext context, IConfiguration configuration, IContactService contactService)
        {
            _context = context;
            _configuration = configuration;
            _contactService = contactService;
        }

        // register and add user with hashed password, for extra protection
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Surname = request.Surname,
                Email = request.Email,
                Password = passwordHash,
                Category = request.Category,
                Phone = request.Phone,
                BirthDate = request.BirthDate
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // return HTTP 200 with created User object
            return Ok(user);

        }

        // login using JWT token
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);

        }


        // get call that returns all users in the database
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _contactService.GetAllUsers();
        }

        // get single user based on his id
        [HttpGet("getSingleUser/{id}")]
        public async Task<ActionResult<User>> getSingleUser(int id)
        {
            var result = _contactService.GetSingleUser(id);
            if(result is null) return NotFound("User not found.");

            return Ok(result);
        }

        // create a new token that expires after 1 day
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
