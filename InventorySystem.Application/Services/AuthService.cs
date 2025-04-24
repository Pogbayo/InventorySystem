using AutoMapper;
using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditLogRepository;

        public AuthService(
            IAuthRepository authRepository,
            IAuditLogRepository auditLogRepository,
            ITokenService tokenGenerator,
            IMapper mapper
            )
        {
            _authRepository = authRepository;
            _tokenGenerator = tokenGenerator;
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
                throw new Exception("Invalid credentials");
            
            var user = await _authRepository.AuthenticateUserAsync(email,password);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var auditLog = new AuditLog(
                action: "Logged In",
                performedBy: user.Id,
                details: $"User {user.Email} logged in."
            );

            await _auditLogRepository.AddLogAsync(auditLog);

            var token = _tokenGenerator.GenerateAccessToken( user);
            return token;
        }

        public async Task<UserGetDto?> RegisterUserAsync(UserCreateDto userdetails)
        {
            var user = new ApplicationUser { Email = userdetails.Email };
            var result = await _authRepository.RegisterUserAsync(user, userdetails.Password);

            if (result == null || !result.Succeeded)
            {
                throw new Exception("Registration failed!");
            }

            var mappedUserObject = _mapper.Map<UserGetDto>(user);
            return mappedUserObject;
        }
    }
}
