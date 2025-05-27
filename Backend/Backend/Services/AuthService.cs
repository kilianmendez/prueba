using Backend.Models.Database.Entities;
using Backend.Models.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Backend.Models.Dtos;

namespace Backend.Services
{
    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TokenValidationParameters _tokenParameters;
        private readonly UserService _userService;

        public AuthService(UnitOfWork unitOfWork, IOptionsMonitor<JwtBearerOptions> jwtOptions, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _tokenParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme)
            .TokenValidationParameters;
            _userService = userService;
        }

        public async Task<string> Login(LoginRequest model)
        {
            User? user = await _unitOfWork.UserRepository.GetByMailAsync(model.Mail);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                //Se añaden los datos necesarios para autorizar al usuario
                Claims = new Dictionary<string, object>
                {
                    { "id", user.Id.ToString() },
                    { ClaimTypes.Email, model.Mail },
                    { ClaimTypes.Role, user.Role.ToString() },
                    { ClaimTypes.Name, user.Name },
                    { ClaimTypes.Surname, user.LastName },
                    { ClaimTypes.MobilePhone, user.Phone }
                },

                //Caducidad del token
                Expires = DateTime.UtcNow.AddHours(1),

                //Especificación de la clave y el algoritmo de firmado
                SigningCredentials = new SigningCredentials(
                    _tokenParameters.IssuerSigningKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };

            //Se crea token y se devuelve al usuario logeado
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public async Task<string> Register(RegisterRequest userRequest)
        {

            LoginRequest model = new LoginRequest
            {
                Mail = userRequest.Mail,
                Password = userRequest.Password,
            };

            await _userService.InsertByMailAsync(userRequest);

            return await Login(model);
        }

        /* OTROS MÉTODOS */
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
