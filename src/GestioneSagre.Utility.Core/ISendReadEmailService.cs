using GestioneSagre.Utility.Domain.Models.ViewModels;

namespace GestioneSagre.Utility.Core;

public interface ISendReadEmailService
{
    Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync();
    Task<EmailMessageViewModel> GetEmailMessageAsync(Guid emailId);
}