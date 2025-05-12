using AutoMapper;
using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Application.Interfaces.IServices;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(
            IAuthRepository authRepository,
            IAuditLogRepository auditLogRepository,
            ITokenService tokenGenerator,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            UserService userService
            )
        {
            _authRepository = authRepository;
            _tokenGenerator = tokenGenerator;
            _auditLogRepository = auditLogRepository;
            _userManager = userManager;
            _userService = userService;
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

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenGenerator.GenerateAccessToken(user, roles);

            return token;
        }

        public async Task<UserGetDto?> RegisterUserAsync(UserCreateDto userdetails)
        {
            try
            {
                if (string.IsNullOrEmpty(userdetails.Email) || string.IsNullOrEmpty(userdetails.Password))
                    throw new ArgumentException("Email or Password cannot be empty.");

                var user = new ApplicationUser
                {
                    Email = userdetails.Email,
                    UserName = userdetails.Email, 
                };

                var result = await _authRepository.RegisterUserAsync(user, userdetails.Password);

                if (result == null || !result.Succeeded)
                {
                    throw new Exception("Registration failed! " + (result?.Errors?.FirstOrDefault()?.Description ?? "Unknown error"));
                }

                var roleResult = await _userService.AddToRoleAsync(user.Id, "User");

                if (roleResult != "User added to role successfully.")
                {
                    throw new Exception("Role not added successfully: " + roleResult);
                }

                var roles = await _userManager.GetRolesAsync(user);

                var mappedUserObject = _mapper.Map<UserGetDto>(user,opt =>
                {
                    opt.Items["RoleNames"] = roles.ToList();
                });

                return mappedUserObject;
            }
            catch (Exception ex)
            {
                // Log the error (you should implement a logger if not already done)
                //_logger.LogError($"Error in RegisterUserAsync: {ex.Message}", ex);
                throw new Exception("Registration failed. Please try again later.",ex);
            }
        }

    }
}
