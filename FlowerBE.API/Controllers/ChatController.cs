
using Microsoft.AspNetCore.Mvc;
using Flower.Contract.Services.Interface;
using Flower.Core;
using Flower.ModelViews.UserModelViews;
using Flower.Core.APIResponse;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IRealTimeService _realTimeService;

    public ChatController(IRealTimeService realTimeService)
    {
        _realTimeService = realTimeService;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        // Kiểm tra nếu UserId của người gửi và người nhận giống nhau (không cho phép gửi tin nhắn cho chính mình)
        if (request.SenderId == request.RecipientId)
        {
            return BadRequest(new { message = "You cannot send messages to yourself." });
        }

        // Tạo tên kênh duy nhất cho cặp người gửi và người nhận, đảm bảo thứ tự không thay đổi
        var userIdStr = request.SenderId.ToString("D"); 
        var recipientUserIdStr = request.RecipientId.ToString("D");  

        // Tạo tên kênh dựa trên thứ tự của các Guid để luôn ổn định
        var channelName = $"chat-{(string.Compare(userIdStr, recipientUserIdStr) < 0 ? userIdStr : recipientUserIdStr)}-{(string.Compare(userIdStr, recipientUserIdStr) < 0 ? recipientUserIdStr : userIdStr)}";

        // Gửi tin nhắn qua Pusher tới kênh duy nhất
        await _realTimeService.SendMessage(channelName, request.Message, request.SenderId, request.RecipientId);

        // Trả về channelName, UserId, RecipientUserId và nội dung tin nhắn cho frontend
        return Ok(new
        {
            message = "Message sent to " + request.RecipientId,
            channelName,
            senderId = request.SenderId,
            receiverId = request.RecipientId,
            messageContent = request.Message,
        });
    }
    [HttpGet("get-message")]
    public async Task<IActionResult> GetMessageHistory([FromQuery] int senderId,[FromQuery] int receiverId)
    {
        try
        {
            var result = await _realTimeService.GetMessageHistory(senderId, receiverId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new Flower.Core.APIResponse.ApiErrorResult<BasePaginatedList<UserModelView>>(ex.Message));
        }
    }

    [HttpGet("chat-partners")]
    public async Task<IActionResult> GetChatPartners([FromQuery] int senderId)
    {
        try
        {
            var partners = await _realTimeService.GetChatPartnersAsync(senderId);
            return Ok(partners);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiErrorResult<UserModelView>(ex.Message));
        }
    }

}

public class SendMessageRequest
{
    public string Message { get; set; }
    public int SenderId { get; set; } 
    public int RecipientId { get; set; }

}
