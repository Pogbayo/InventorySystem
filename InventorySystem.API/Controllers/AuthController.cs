using InventorySystem.API.Common;
using InventorySystem.Application.DTOs.ApllicationUserDto;
using InventorySystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(SignInDto loginDeets)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDeets.Email, loginDeets.Password);

                if (!string.IsNullOrEmpty(result))
                {
                    return OkResponse(result, "Login successful");
                }
                else
                {
                    return ErrorResponse("Invalid credentials", "Failed to log in. Please check your email and password.");
                }
            }
            catch (Exception ex)
            {
                return ErrorResponse("Login failed", ex.Message);
            }
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<ApiResponse<UserGetDto>>> SignUp(UserCreateDto userdeets)
        {
            var user = await _authService.RegisterUserAsync(userdeets);
            if (user == null)
            {
                return ErrorResponse("User registration failed");
            }
            return OkResponse(user,"User created successfully");
        }
    }
}
