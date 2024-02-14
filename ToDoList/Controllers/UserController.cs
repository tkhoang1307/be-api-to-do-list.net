using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.RequestModels.User;
using ToDoList.Service.IServices;
using ToDoList.Service.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpRequestModel userSignUpRequest)
        {
            var response = await _userService.SignUpAccount(userSignUpRequest);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var resAllUsers = await _userService.GetAllUsers();

            if (resAllUsers == null)
            {
                return NotFound();
            }

            return Ok(resAllUsers);
        }

        [AllowAnonymous]
        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> GetUserByUserId(Guid userId)
        {
            var response = await _userService.GetUserByUserId(userId);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPatch("{userId:Guid}")]
        public async Task<IActionResult> UpdateUserInformation(Guid userId, [FromBody] UserUpdateInformationRequestModel userUpdatedRequest)
        {
            var response = await _userService.UpdateUserInformation(userId, userUpdatedRequest);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
