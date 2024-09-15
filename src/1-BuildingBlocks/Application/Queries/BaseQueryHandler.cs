using AutoMapper;

namespace TaskoMask.BuildingBlocks.Application.Queries;

/// <summary>
///
/// </summary>
public abstract class BaseQueryHandler
{
    #region Fields

    protected readonly IMapper Mapper;

    #endregion


    #region Ctors


    protected BaseQueryHandler(IMapper mapper)
    {
        Mapper = mapper;
    }

    #endregion


    #region Protected Methods



    #endregion
}
