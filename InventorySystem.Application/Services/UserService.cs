using AutoMapper;
using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.Interfaces.IRepositories;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InventorySystem.Application.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        //private readonly IAuditLogRepository _auditLogRepository;
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
            //_auditLogRepository = auditLogRepository;
            _userManager = userManager;
            _currentuser = currentuser;
        }

        public async Task<UserGetDto?> GetByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var mappedUser = _mapper.Map<UserGetDto>(user, opt =>
            {
                opt.Items["RoleNames"] = roles.ToList();
            });

            return mappedUser;
        }


        public async Task<List<UserGetDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users
                .Select(user => new UserGetDto
                {
                    Id = user.Id.ToString(),
                    Email = user.Email
                })
                .ToListAsync();

            return users;
        }


        public async Task<UserGetDto?> GetByEmailAsync(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var mappedUser = _mapper.Map<UserGetDto>(user, opt =>
            {
                opt.Items["RoleNames"] = roles.ToList();
            });

            return mappedUser;
        }

        public async Task<string> UpdateAsync(UpdateUserDto userDto)
        {
            //var currentUserId = _currentuser.GetUserId();

            var user = await _userManager.FindByIdAsync(userDto.Id!);
            if (user == null)
            {
                return "User not found";
            }

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            //var auditLog = new AuditLog(
            //    action: $"User with ID {user.Id} updated",
            //    performedBy: currentUserId,
            //    details: $"{currentUserId} updated user with ID: {user.Id}"
            //);

            //await _auditLogRepository.AddLogAsync(auditLog);

            return "User updated successfully";
        }

        public async Task<ApplicationUser?> GetApplicationUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<string> DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            //var currentuserId = _currentuser.GetUserId();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var result  = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return "User deleted successfully";
            }

            //var auditLog = new AuditLog(
            //  action: $"User with id{user.Id} deleted",
            //  performedBy: currentuserId,
            //  details: $"{currentuserId} Deleted user with id: {user.Id}"
            //);

            //await _auditLogRepository.AddLogAsync(auditLog);


            return "Failed to delete user";
        }

        public async Task<string> AddToRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            //var currentuserId = _currentuser.GetUserId();

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
                //var auditLog = new AuditLog(
                //  action: $"Rolename{roleName} was added to User with id {user.Id}",
                //  performedBy: currentuserId,
                //  details: $"{currentuserId} added {roleName} to User: {user.Id}"
                //);

                //await _auditLogRepository.AddLogAsync(auditLog);

                return "User added to role successfully.";
            }
            else
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }
        }


        public async Task<IdentityResult> RemoveFromRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
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

            //if (result.Succeeded)
            //{
            //    var auditLog = new AuditLog(
            //        action: $"Role {roleName} was removed from User with id {user.Id}",
            //        performedBy: currentUserId,
            //        details: $"{currentUserId} removed {roleName} from User: {user.Id}"
            //    );
            //    await _auditLogRepository.AddLogAsync(auditLog);
            //}

            return result;
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
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

        public async Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(Guid userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }

            //var currentUserId = _currentuser.GetUserId();
            var result = await _userManager.CheckPasswordAsync(user, password);

            //var auditLog = new AuditLog(
            //    action: $"Password check performed for User with id {user.Id}",
            //    performedBy: currentUserId,
            //    details: $"{currentUserId} checked password for User: {user.Id}. Result: {(result ? "Success" : "Failure")}"
            //);
            //await _auditLogRepository.AddLogAsync(auditLog);

            return result;
        }

    }
}

