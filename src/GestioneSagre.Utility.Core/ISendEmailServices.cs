namespace GestioneSagre.Utility.Core;

public interface ISendEmailServices
{
    Task<bool> UpdateEmailStatusAsync(int id, Guid emailId, int status);
}