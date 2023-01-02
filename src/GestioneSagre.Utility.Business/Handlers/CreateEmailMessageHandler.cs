using AutoMapper;
using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Services;
using GestioneSagre.Utility.Infrastructure.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Business.Handlers;

public class CreateEmailMessageHandler : IRequestHandler<CreateEmailMessageCommand, bool>
{
    private readonly ISendEmailService sendEmailWriteService;
    private readonly ILogger logger;

    public CreateEmailMessageHandler(ISendEmailService sendEmailWriteService, IMapper mapper, ILogger<CreateEmailMessageHandler> logger)
    {
        this.sendEmailWriteService = sendEmailWriteService;
        this.logger = logger;
    }

    public async Task<bool> Handle(CreateEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new EmailMessage(0, request.EmailId, request.Recipient, request.RecipientEmail,
            request.Subject, request.Message, request.SendDate, request.EffectiveSendDate, request.EmailSendCount,
            request.Status);

        logger.LogInformation("Creating a new record for email {email}", request.EmailId);
        var result = await sendEmailWriteService.CreateEmailMessage(entity);

        if (!result)
        {
            logger.LogWarning("Error creating new record for email {email}", request.EmailId);
            return false;
        }

        return await Task.FromResult(true);
    }
}
