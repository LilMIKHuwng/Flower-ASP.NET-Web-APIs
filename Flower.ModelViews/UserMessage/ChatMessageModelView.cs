
using Flower.ModelViews.UserModelViews;

namespace Flower.ModelViews.UserMessage
{
    public class ChatMessageModelView
    {
        public int Id { get; set; }
        public UserModelView SenderId { get; set; }
        public UserModelView ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime SendAt { get; set; }
    }
}
