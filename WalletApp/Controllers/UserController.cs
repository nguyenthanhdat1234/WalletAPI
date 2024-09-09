using Microsoft.AspNetCore.Mvc;
using WalletProject.Models;

namespace WalletApp.Controllers
{
    public class UserController : Controller
    {
        public readonly WalletDbContext _context;

        public UserController(WalletDbContext context)
        {
            _context = context;
        }

        [HttpPost("users/Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            if(string.IsNullOrEmpty(username)|| string.IsNullOrEmpty(password))
            {
                return BadRequest("Username or password not empty.");
            }

            var user = new User
            {
                UserName = username,
                Password = password
            };

             _context.Users.Add(user);
            await _context.SaveChangesAsync();



            var wallet = new Wallet
            {
                UserId = user.UsertId,
                Point = 0,
                CreateAt = DateTime.UtcNow,
            };
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return Ok("User is created.");
        }

    }
}
