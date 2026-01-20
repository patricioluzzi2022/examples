using Library.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Actions
{
    public class AuthQuery
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Nickname = "TreeKeys", Password = "234 678" }
        };

        private const string SecretKey = "234colors&numbers ";

        public AuthQuery()
        {

        }

        private User ValidateUser(string nickname, string password)
        {
            return _users.FirstOrDefault(u => u.Nickname == nickname && u.Password == password);
        }
        
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Code", user.Code.ToString()),
                new Claim("Nickname", user.Nickname.ToString()),
                new Claim("Password", user.Password.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "tuapp.com",
                audience: "tuapp.com",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public AuthResponse Login(string email,string password)
        {
            var user = ValidateUser(email, password);
            if (user == null)
            {
                throw new GraphQLException("Credenciales inválidas");
            }

            var token = GenerateToken(user);
            return new AuthResponse
            {
                Token = token,
                User = new User { Id = user.Id, Code = user.Code, Nickname = user.Nickname }
            };
        }
    }
}
