using TechGRC.Server.Models.DTO;

namespace TechGRC.Server.Services
{
    public interface IChatService
    {
        Task<string> ProcessPromptAsync(string userPrompt);
    }
}
