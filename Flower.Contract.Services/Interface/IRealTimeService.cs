using Flower.ModelViews.UserMessage;
using Flower.ModelViews.UserModelViews;

namespace Flower.Contract.Services.Interface
{
    public interface IRealTimeService
    {
        Task SendMessage(string channel, string message, int senderId, int receiverId);
        Task<List<ChatMessageModelView>> GetMessageHistory(int senderId, int receiverId);
        Task<List<UserModelView>> GetChatPartnersAsync(int senderId);
    }
}

