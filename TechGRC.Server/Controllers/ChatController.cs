using Microsoft.AspNetCore.Mvc;
using TechGRC.Server.Models.DTO;
using TechGRC.Server.Services;

namespace TechGRC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPrompt(ChatRequestDto request)
        {
            try
            {
                var response = await _chatService.ProcessPromptAsync(request.Message);
                return Ok(new ChatResponseDto { Response = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to process prompt", details = ex.Message });
            }
        }
    }

}
