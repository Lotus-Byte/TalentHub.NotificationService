using TalentHub.NotificationService.Infrastructure.Abstractions;
using TalentHub.NotificationService.Infrastructure.Data;
using TalentHub.NotificationService.Infrastructure.Models;

namespace TalentHub.NotificationService.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _context;
    
    public NotificationRepository(NotificationDbContext context) => _context = context;
    
    public async Task<NotificationMessage?> AddNotificationAsync(NotificationMessage notificationMessage)
    {
        ArgumentNullException.ThrowIfNull(notificationMessage);
        
        await _context.Notifications.AddAsync(notificationMessage);
        await _context.SaveChangesAsync();
        
        return notificationMessage;
    }
}