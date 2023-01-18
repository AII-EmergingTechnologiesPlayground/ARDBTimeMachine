using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbTimeMachine.API.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace DbTimeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DbTimeMachineContext _context;

        public AccountsController(DbTimeMachineContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpPost("Login")]
        public ActionResult<bool> GetAccount(Account account)
        {
            string passwordHash = HashPassword(account.PasswordHash, account.Email);
            account.PasswordHash = passwordHash;
            bool correctAccount = _context.Accounts.Count(a => a.Email == account.Email && a.PasswordHash == account.PasswordHash) > 0;

            return correctAccount;
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            string passwordHash = HashPassword(account.PasswordHash, account.Email);
            account.PasswordHash = passwordHash;
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        private string HashPassword(string pass, string email)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(email);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass!,
                salt: bytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
