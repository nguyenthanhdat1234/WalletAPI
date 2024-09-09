using Microsoft.AspNetCore.Mvc;
using WalletProject.Models;

namespace WalletProject.Controllers
{
    public class WalletController : Controller
    {
        public readonly WalletDbContext _context;


        public WalletController(WalletDbContext context)
        {
            _context = context;
        }

        [HttpPost("{userId}/createwallet")]
        public async Task<IActionResult> CreateWallet(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (user.Wallets != null)
            {
                return NotFound("User already has a wallet.");
            }
            var wallet = new Wallet
            {
                UserId = userId,
                Point = 0,
                CreateAt = DateTime.UtcNow,
            };

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return Ok(new { wallet.WalletId, wallet.Point });
        }

        [HttpGet("{userId}/Point")]

        public async Task<IActionResult> getWalletPoint(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var wallet = _context.Wallets.FirstOrDefault(x => x.UserId == userId);

            if (wallet == null)
            {
                return NotFound("Wallet not found.");
            }
            return Ok(wallet.Point);
        }

        [HttpPost("wallets/deposit")]
        public async Task<IActionResult> Deposit(int point, int userId)
        {

            var user = _context.Wallets.FirstOrDefault(x => x.UserId == userId);
            var wallet = _context.Wallets.FirstOrDefault(x => x.UserId == userId);
            if (user == null || wallet == null)
            {
                return NotFound("User or wallet not found.");
            }
            if (point.CompareTo(0) <= 0)
            {
                return BadRequest("Point need to be greater than 0.");
            }
            var transaction = new Transaction
            {
                TransactionName = "Thanh toan Test",
                Category = TransactionEnum.Categories.Other,
                DateOfTransaction = DateTime.UtcNow,
                ModeOfPayment = TransactionEnum.ModeOfPayment.Bank,
                VendorName = "Test",
                TransactionCost = point,
                TransactionCostCharge = 10,
                WalletId = wallet.WalletId,
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            wallet.Point += point;

            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        [HttpPost("wallets/withdraw")]
        public async Task<IActionResult> Withdraw(int userId, [FromBody] int point)
        {
            var user = _context.Users.Find(userId);
            // Fetch user and wallet in one go, avoid extra queries if either is null
            var wallet = _context.Wallets.FirstOrDefault(x => x.UserId == userId);

            if (wallet == null || user == null)
            {
                return NotFound(wallet == null ? "Wallet not found." : "User not found.");
            }

            // Check if the point is greater than 0
            if (point <= 0)
            {
                return BadRequest("Point needs to be greater than 0.");
            }

            // Check if wallet has enough points
            if (wallet.Point < point)
            {
                return BadRequest("The withdrawal amount must be less than or equal to the current account balance.");
            }

            // Deduct points and update the wallet
            wallet.Point -= point;
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();

            return Ok(new { RemainingBalance = wallet.Point });
        }



    }

}
