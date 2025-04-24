using AutoMapper;
using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Application.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CurrentUserService _currentuser;
        public UserService(
            IMapper mapper,
            IAuditLogRepository auditLogRepository,
            UserManager<ApplicationUser> userManager,
            CurrentUserService currentuser
            )
        {
            _mapper = mapper;
            _auditLogRepository = auditLogRepository;
            _userManager = userManager;
            _currentuser = currentuser;
        }

        public async Task<UserGetDto?> GetByIdAsync(string userId)
        {
            var currentuserId = _currentuser.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var mappedUser = _mapper.Map<UserGetDto>(user);

            var auditLog = new AuditLog(
                action: $"Fetched {userId}",
                performedBy: currentuserId,
                details: $"{currentuserId} fetched user with id {userId}"
            );

            await _auditLogRepository.AddLogAsync(auditLog);

            return mappedUser;
        }

        public async Task<UserGetDto?> GetByEmailAsync(string email)
        {
            var currentuserId = _currentuser.GetUserId();

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var user = await _userManager.FindByEmailAsync(email);

            var mappedUser = _mapper.Map<UserGetDto>(user);

            var auditLog = new AuditLog(
               action: $"Fetched user with email address:{email}",
               performedBy: currentuserId,
               details: $"{currentuserId} fetched user with eamil address: {email}"
           );

            await _auditLogRepository.AddLogAsync(auditLog);

            return mappedUser;
        }

        public async Task<string> UpdateAsync(ApplicationUser user)
        {
            var currentuserId = _currentuser.GetUserId();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var result =  await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return "User updatedsuccessfully";
            }

            var auditLog = new AuditLog(
              action: $"User with id{user.Id} updated",
              performedBy: currentuserId,
              details: $"{currentuserId} Updated user with id: {user.Id}"
            );

            await _auditLogRepository.AddLogAsync(auditLog);

            return "User update failed";
        }

        public async Task<string> DeleteAsync(ApplicationUser user)
        {
            var currentuserId = _currentuser.GetUserId();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var result  = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return "User updatedsuccessfully";
            }

            var auditLog = new AuditLog(
              action: $"User with id{user.Id} deleted",
              performedBy: currentuserId,
              details: $"{currentuserId} Deleted user with id: {user.Id}"
            );

            await _auditLogRepository.AddLogAsync(auditLog);


            return "Failed to delete user";
        }

        public async Task<string> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var currentuserId = _currentuser.GetUserId();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty", nameof(roleName));
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                var auditLog = new AuditLog(
                  action: $"Rolename{roleName} was added to User with id {user.Id}",
                  performedBy: currentuserId,
                  details: $"{currentuserId} added {roleName} to User: {user.Id}"
                );

                await _auditLogRepository.AddLogAsync(auditLog);

                return "User added to role successfully.";
            }
            else
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }
        }


        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty", nameof(roleName));
            }

            var currentUserId = _currentuser.GetUserId();
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                var auditLog = new AuditLog(
                    action: $"Role {roleName} was removed from User with id {user.Id}",
                    performedBy: currentUserId,
                    details: $"{currentUserId} removed {roleName} from User: {user.Id}"
                );
                await _auditLogRepository.AddLogAsync(auditLog);
            }

            return result;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty", nameof(roleName));
            }

            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }

            var currentUserId = _currentuser.GetUserId();
            var result = await _userManager.CheckPasswordAsync(user, password);

            var auditLog = new AuditLog(
                action: $"Password check performed for User with id {user.Id}",
                performedBy: currentUserId,
                details: $"{currentUserId} checked password for User: {user.Id}. Result: {(result ? "Success" : "Failure")}"
            );
            await _auditLogRepository.AddLogAsync(auditLog);

            return result;
        }

    }
}

