using OperaHouseApp.Data;
using OperaHouseApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.Authentication
{
    public class AuthenticationService
    {
        private ApplicationContext _context;

        public AuthenticationService(ApplicationContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            User user = _context.GetUserByUsername(username);

            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }

            return null;
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Simplified: Assume the storedHash is the direct password for simplicity
            // In a real-world application, you would hash the inputPassword and compare it to the stored hash
            return inputPassword == storedHash;
        }

    }
}
