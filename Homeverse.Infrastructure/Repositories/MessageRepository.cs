﻿using Homeverse.Domain.Entities;
using Homeverse.Domain.Interfaces;
using Homeverse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Homeverse.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly HomeverseDbContext _context;

    public MessageRepository(HomeverseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Message>> GetMessageThreadAsync(int userId, int otherId)
    {
        return await _context.Messages
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => (x.SenderId == userId && x.ReceiverId == otherId) || (x.SenderId == otherId && x.ReceiverId == userId))
            .ToListAsync();
    }

    public async Task<Message> GetMessageByIdAsync(int id)
    {
        return await _context.Messages
                        .Include(x => x.Sender)
                        .Include(x => x.Receiver)
                        .Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddMessageAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
    }

    public async Task ReadMessageThreadAsync(int userId, int otherId)
    {
        await _context.Messages
            .Where(x => (x.SenderId == otherId && x.ReceiverId == userId) && x.IsReaded == false)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsReaded, true));
    }
}
