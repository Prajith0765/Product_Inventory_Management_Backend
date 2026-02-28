using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Application.Service
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthService(IAuthRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<string?> LoginAsync(LoginDTO loginDto)
        {
            var user = await _repository.ValidateUserAsync(loginDto.username);

            if (user == null)
            {
                return null;
            }

            if (user.ProductUserPasswordHash != loginDto.password)
            {
                return null;
            }

            return _jwtService.GenerateToken(user);

        }
    }
}
