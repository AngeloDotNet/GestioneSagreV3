using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Business.Handlers;

public class DeleteEmailMessageHandler : IRequestHandler<DeleteEmailMessageCommand, bool>
{
    private readonly ISendEmailService sendEmailWriteService;
    private readonly ILogger logger;

    public DeleteEmailMessageHandler(ISendEmailService sendEmailWriteService, ILogger<DeleteEmailMessageHandler> logger)
    {
        this.sendEmailWriteService = sendEmailWriteService;
        this.logger = logger;
    }

    public async Task<bool> Handle(DeleteEmailMessageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting record for email {email}", request.EmailId);
        var result = await sendEmailWriteService.DeleteEmailMessage(request.Id, request.EmailId);

        if (!result)
        {
            logger.LogWarning("Error deleting record for email {email}", request.EmailId);
            return false;
        }

        return await Task.FromResult(true);
    }
}