﻿using MassTransit;
using System.Threading.Tasks;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.BuildingBlocks.Web.MVC.Exceptions;
using TaskoMask.Services.Identity.Api.UseCases.UpdateUser;

namespace TaskoMask.Services.Identity.Api.Consumers;

/// <summary>
/// After registering an owner, we must register a user for that owner to handle its identity (login,logout,etc)
/// </summary>
public class OwnerProfileUpdatedConsumer : BaseConsumer<OwnerProfileUpdated>
{
    public OwnerProfileUpdatedConsumer(IInMemoryBus inMemoryBus)
        : base(inMemoryBus) { }

    public override async Task ConsumeMessage(ConsumeContext<OwnerProfileUpdated> context)
    {
        var registerUser = new UpdateUserRequest(context.Message.OldEmail, context.Message.NewEmail);
        var result = await InMemoryBus.SendCommand(registerUser);
        if (!result.IsSuccess)
            throw new MessageConsumerFaultException(result.Message); // Cause to publish Fault<OwnerProfileUpdated> message
    }
}
