﻿using PusherServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Flower.Contract.Services.Interface;
using Flower.Repositories.Entity;
using Flower.Contract.Repositories.Entity;
using Flower.Contract.Repositories.Interface;
using Flower.ModelViews.UserMessage;
using Flower.ModelViews.UserModelViews;

public class RealTimeService : IRealTimeService
{
    private readonly Pusher _pusher;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public RealTimeService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork)
    {
        _pusher = new Pusher("1964573", "01567a69c62f53eeceb1", "2a5e8270339a5c65862a", new PusherOptions
        {
            Cluster = "ap1",
            Encrypted = true
        });
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task SendMessage(string channel, string message, int senderId, int receiverId)
    {
        try
        {
            // Gửi tin nhắn qua Pusher
            var result = await _pusher.TriggerAsync(channel, "new-message", new
            {
                messageContent = message, 
                senderId = senderId,
                receiverId = receiverId
            });
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            var chatMessage = new UserMessage
            {
                SenderId = senderId,
                RecipientId = receiverId,
                MessageContent = message,
                SendAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone),
                ChannelName = channel
            };

            var messageRepo = _unitOfWork.GetRepository<UserMessage>();
            await messageRepo.InsertAsync(chatMessage);
            await _unitOfWork.SaveAsync();

            // Không cần kiểm tra kết quả vì Pusher TriggerAsync sẽ ném ra ngoại lệ nếu có lỗi
        }
        catch (Exception ex)
        {
            // Nếu có lỗi xảy ra trong quá trình gửi tin nhắn, ném lỗi hoặc xử lý theo cách bạn muốn
            throw new Exception("Failed to send message via Pusher.", ex);
        }
    }


    public async Task<List<ChatMessageModelView>> GetMessageHistory(int senderId, int receiverId)
    {
        var messages = await _unitOfWork.GetRepository<UserMessage>().Entities
            .Where(m => (m.SenderId == senderId && m.RecipientId == receiverId) ||
                        (m.SenderId == receiverId && m.RecipientId == senderId))
            .OrderBy(m => m.SendAt)
            .Include(m => m.Sender) // Người gửi
            .Include(m => m.Recipient) // Người nhận
            .ToListAsync();

        var mappedMessages = messages.Select(m => new ChatMessageModelView
        {
            Id = m.Id,
            SenderId = _mapper.Map<UserModelView>(m.Sender),
            ReceiverId = _mapper.Map<UserModelView>(m.Recipient),
            SendAt = m.SendAt,
            Message = m.MessageContent,
            
        }).ToList();

        return mappedMessages;
    }


    public async Task<List<UserModelView>> GetChatPartnersAsync(int senderId)
    {
        var messageRepo = _unitOfWork.GetRepository<UserMessage>();

        var messages = await messageRepo.Entities
            .Where(m => m.SenderId == senderId || m.RecipientId == senderId)
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .ToListAsync();

        // Lấy các đối tác trò chuyện (người còn lại trong cuộc trò chuyện)
        var partnerUsers = messages
            .Select(m =>
                m.SenderId == senderId ? m.Recipient : m.Sender
            )
            .DistinctBy(u => u.Id) // đảm bảo không trùng lặp
            .ToList();

        return _mapper.Map<List<UserModelView>>(partnerUsers);
    }

}
