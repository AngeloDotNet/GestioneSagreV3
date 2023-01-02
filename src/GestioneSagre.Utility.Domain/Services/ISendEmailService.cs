using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Domain.Services;

public interface ISendEmailService
{
    Task<bool> CreateEmailMessage(EmailMessage request);
    Task<bool> UpdateEmailMessage(EmailMessage request);
    Task<bool> DeleteEmailMessage(int id, Guid emailId);
}