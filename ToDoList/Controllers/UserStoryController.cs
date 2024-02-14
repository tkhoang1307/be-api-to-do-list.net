using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Model.RequestModels.User;
using ToDoList.Model.RequestModels.UserStory;
using ToDoList.Service.IServices;
using ToDoList.Service.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStoryController : ControllerBase
    {
        private readonly IUserStoryService _userStoryService;

        public UserStoryController(IUserStoryService userStoryService)
        {
            _userStoryService = userStoryService;
        }

        [AllowAnonymous]
        [HttpGet("user/{userId:Guid}")]
        public async Task<IActionResult> GetUserStoryByUser(Guid userId)
        {
            var response = await _userStoryService.GetUserStoryByUserId(userId);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("{usId:Guid}")]
        public async Task<IActionResult> GetUserStoryByUSId(Guid usId)
        {
            var response = await _userStoryService.GetUserStoryByUSId(usId);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserStory([FromBody] UserStoryCreationRequestModel userStoryCreationRequest)
        {
            var response = await _userStoryService.CreateUserStory(userStoryCreationRequest);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPatch("{usId:Guid}")]
        public async Task<IActionResult> UpdateUserStory(Guid usId, [FromBody] UserStoryUpdationRequestModel userStoryUpdationRequest)
        {
            var response = await _userStoryService.UpdateUserStory(usId, userStoryUpdationRequest);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
