using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Core;

public class SendReadEmailService : ISendReadEmailService
{
    private readonly ILogger<SendEmailServices> logger;
    private readonly ReadDbContext readDbContext;

    public SendReadEmailService(ILogger<SendEmailServices> logger, ReadDbContext readDbContext)
    {
        this.logger = logger;
        this.readDbContext = readDbContext;
    }

    public async Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync()
    {
        try
        {
            logger.LogInformation("GetAllEmailMessagesAsync - Start");
            var baseQuery = readDbContext.EmailMessages
                .Where(x => x.Status == EmailStatus.Pending)
                .AsNoTracking();

            logger.LogInformation("GetAllEmailMessagesAsync - End");
            var messages = await baseQuery
                .Select(email => EmailMessageViewModel.FromEntity(email))
                .ToListAsync();

            return messages;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading email messages");
            throw;
        }
    }

    public async Task<EmailMessageViewModel> GetEmailMessageAsync(Guid emailId)
    {
        try
        {
            logger.LogInformation("GetEmailMessageAsync - Start");
            var baseQuery = readDbContext.EmailMessages
                .AsNoTracking()
                .Where(x => x.EmailId == emailId)
                .Select(email => EmailMessageViewModel.FromEntity(email));

            logger.LogInformation("GetEmailMessageAsync - End");
            var message = await baseQuery.FirstOrDefaultAsync();

            return message;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading email message");
            throw;
        }
    }
}
