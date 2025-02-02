using Microsoft.EntityFrameworkCore;
using TalentHub.NotificationService.Infrastructure.Models;

namespace TalentHub.NotificationService.Infrastructure.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(){}
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
    
    public DbSet<NotificationMessage> Notifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы Notifications
        modelBuilder.Entity<NotificationMessage>()
            .ToTable("Notifications");

        base.OnModelCreating(modelBuilder);
    }
}