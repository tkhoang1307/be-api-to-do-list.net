using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Model.RequestModels.Status;
using ToDoList.Model.RequestModels.UserStory;
using ToDoList.Service.IServices;
using ToDoList.Service.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController: ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var response = await _statusService.GetAllStatuses();

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewStatus([FromBody] StatusCreationModel statusCreationData)
        {
            var response = await _statusService.CreateNewStatus(statusCreationData);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPatch("{statusId:Guid}")]
        public async Task<IActionResult> UpdateStatus(Guid statusId, [FromBody] StatusUpdationRequestModel statusUpdationRequest)
        {
            var response = await _statusService.UpdateStatus(statusId, statusUpdationRequest);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
