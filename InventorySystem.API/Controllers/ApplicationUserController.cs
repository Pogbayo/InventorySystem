using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventorySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseController
    {
        private readonly UserService _userService;
        public ApplicationUserController( UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all-users")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResponse<List<UserGetDto>>>> GetAllUsers()
        {
            Console.WriteLine("GetAllUsers endpoint reached");

            var user = HttpContext.User; 

            Console.WriteLine("HttpContext User: " + user?.Identity?.Name); 

            foreach (var claim in user?.Claims ?? Enumerable.Empty<Claim>())
            {
                Console.WriteLine($"Claim: {claim.Type} - {claim.Value}");
            }

            if (user == null )
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = user.FindFirstValue(ClaimTypes.Email);
            var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            Console.WriteLine($"User ID: {userId}, Email: {userEmail}, Roles: {string.Join(", ", roles)}");

            var users = await _userService.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return ErrorResponse("Users list is empty", "No users found in the system.");
            }
            return OkResponse(users, "users retrieved successfully.");
        }


        [HttpGet("get-user-by-/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ApiResponse<UserGetDto>>> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(user, "User successfully retrieved");
        } 
        
        [HttpGet("get-user-by-email/{email}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ApiResponse<UserGetDto>>> GetUserByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFoundResponse();
            }
            return OkResponse(user, "User successfully retrieved");
        }

        [HttpPut("update-user-by-id")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateUser( [FromBody] UpdateUserDto userDto)
        {
            var result = await _userService.UpdateAsync(userDto);
            if (result != "User updated successfully")
            {
                return ErrorResponse("Error updating user", result);
            }

            return OkResponse(result, "User updated successfully");
        }


        [HttpDelete("delete-user-by/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id.ToString());
            if (result != "User deleted successfully")
            {
                return ErrorResponse("Error deleting user");
            }

            return OkResponse(result);
        }

        [HttpPost("add-user-to-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToRole([FromBody] AddToRoleDto model)
        {
            var user = await _userService.GetByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                return NotFoundResponse("User not found");
            }

            var result = await _userService.AddToRoleAsync(model.UserId ,model.RoleName);

            if (result == "User added to role successfully.")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }


        [HttpPost("remove-from-role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<string>>> RemoveFromRole([FromQuery] Guid userId, [FromQuery] string roleName)
        {
            var user = await _userService.GetByIdAsync(userId.ToString());
            if (user == null)
                return NotFoundResponse("User not found");

            var result = await _userService.RemoveFromRoleAsync(userId, roleName);
            if (!result.Succeeded)
                return ErrorResponse("Failed to remove user from role");

            return OkResponse("User removed from role successfully");
        }


        [HttpGet("is-in-role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<object>>> IsUserInRole([FromQuery] Guid userId, [FromQuery] string roleName)
        {
            try
            {
                var isInRole = await _userService.IsInRoleAsync(userId, roleName);
                var result = new
                {
                    userId,
                    roleName,
                    isInRole
                };
                return OkResponse(result);
            }
            catch (ArgumentNullException ex)
            {
                return ErrorResponse(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return ErrorResponse(ex.Message);
            }
        }


        [HttpGet("get-user-roles")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IList<string>>>> GetUserRoles([FromQuery] Guid userId)
        {
            try
            {
                var roles = await _userService.GetUserRolesAsync(userId);
                return OkResponse(roles);
            }
            catch (ArgumentNullException ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost("check-password")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CheckPassword([FromBody] CheckPasswordDto model)
        {
            try
            {
                var user = await _userService.GetApplicationUserByIdAsync(model.UserId.ToString());
                if (user == null)
                {
                    return NotFoundResponse("User not found");
                }

                var result = await _userService.CheckPasswordAsync(model.UserId, model.Password);
                return OkResponse(result);
            }
            catch (ArgumentNullException ex)
            {
                return ErrorResponse(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

    }
}
