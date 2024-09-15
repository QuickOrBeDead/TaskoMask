﻿using AutoMapper;
using MassTransit;
using System.Threading.Tasks;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Cards;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Contracts.Protos;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.Services.Tasks.Read.Api.Infrastructure.DbContext;
using static TaskoMask.BuildingBlocks.Contracts.Protos.GetCardByIdGrpcService;

namespace TaskoMask.Services.Tasks.Read.Api.Consumers.Tasks;

public class TaskAddedConsumer : BaseConsumer<TaskAdded>
{
    private readonly GetCardByIdGrpcServiceClient _getCardByIdGrpcServiceClient;
    private readonly TaskReadDbContext _taskReadDbContext;
    private readonly IMapper _mapper;

    public TaskAddedConsumer(
        IInMemoryBus inMemoryBus,
        TaskReadDbContext taskReadDbContext,
        IMapper mapper,
        GetCardByIdGrpcServiceClient getCardByIdGrpcServiceClient
    )
        : base(inMemoryBus)
    {
        _taskReadDbContext = taskReadDbContext;
        _mapper = mapper;
        _getCardByIdGrpcServiceClient = getCardByIdGrpcServiceClient;
    }

    /// <summary>
    ///
    /// </summary>
    public override async Task ConsumeMessage(ConsumeContext<TaskAdded> context)
    {
        var card = await GetCardFromRpcClientAsync(context.Message.CardId);

        var task = new Domain.Task(context.Message.Id)
        {
            Title = context.Message.Title,
            Description = context.Message.Description,
            CardId = context.Message.CardId,
            ProjectId = card.ProjectId,
            OrganizationId = card.OrganizationId,
            OwnerId = card.OwnerId,
            CardName = card.Name,
            CardType = card.Type,
        };

        await _taskReadDbContext.Tasks.InsertOneAsync(task);
    }

    /// <summary>
    ///
    /// </summary>
    private async Task<GetCardDto> GetCardFromRpcClientAsync(string cardId)
    {
        var cardGrpcResponse = await _getCardByIdGrpcServiceClient.HandleAsync(new GetCardByIdGrpcRequest { Id = cardId });

        return _mapper.Map<GetCardDto>(cardGrpcResponse);
    }
}
