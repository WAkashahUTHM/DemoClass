using Microsoft.EntityFrameworkCore;
using TechGRC.Server.Data;
using TechGRC.Server.Models;
using TechGRC.Server.Models.DTO;

using OpenAI.Chat;

namespace TechGRC.Server.Services
{

    public class ChatService : IChatService
    {
        private readonly ChatClient _chatClient;

        public ChatService(ChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<string> ProcessPromptAsync(string userPrompt)
        {
            try
            {
                var response = await _chatClient.CompleteChatAsync(
                    new ChatMessage[]
                    {
                        new SystemChatMessage("You are a helpful assistant."),
                        new UserChatMessage(userPrompt)
                    }
                );

                return response.Value.Content[0].Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing prompt: {ex.Message}", ex);
            }
        }
    }
}
