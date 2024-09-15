using MassTransit;
using TaskoMask.BuildingBlocks.Application.Bus;

namespace TaskoMask.BuildingBlocks.Web.MVC.Consumers;

public abstract class BaseConsumer<TEvent> : IConsumer<TEvent>
    where TEvent : class
{
    #region Fields

    protected readonly IInMemoryBus InMemoryBus;

    #endregion

    #region Ctors



    public BaseConsumer(IInMemoryBus inMemoryBus)
    {
        InMemoryBus = inMemoryBus;
    }

    #endregion

    #region Public Methods



    /// <summary>
    ///
    /// </summary>

    public abstract Task ConsumeMessage(ConsumeContext<TEvent> context);

    /// <summary>
    ///
    /// </summary>
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        await ConsumeMessage(context);
    }

    #endregion

    #region Private Methods


    #endregion
}
