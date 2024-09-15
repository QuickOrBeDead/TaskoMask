using TaskoMask.BuildingBlocks.Contracts.Dtos.Boards;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.ViewModels;
using TaskoMask.BuildingBlocks.Web.Services.Http;

namespace TaskoMask.Clients.UserPanel.Services.API;

public class BoardApiService : BaseApiService
{
    #region Fields


    #endregion

    #region Ctor

    public BoardApiService(IHttpClientService httpClientService)
        : base(httpClientService) { }

    #endregion

    #region Public Methods




    /// <summary>
    ///
    /// </summary>
    public async Task<Result<GetBoardDto>> GetAsync(string id)
    {
        var url = $"/boards/{id}";
        return await HttpClientService.GetAsync<GetBoardDto>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<BoardDetailsViewModel>> GetDetailsAsync(string id)
    {
        var url = $"/boards/{id}/details";
        return await HttpClientService.GetAsync<BoardDetailsViewModel>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> AddAsync(AddBoardDto input)
    {
        var url = $"/boards";
        return await HttpClientService.PostAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> UpdateAsync(string id, UpdateBoardDto input)
    {
        var url = $"/boards/{id}";
        return await HttpClientService.PutAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> DeleteAsync(string id)
    {
        var url = $"/boards/{id}";
        return await HttpClientService.DeleteAsync<CommandResult>(url);
    }

    #endregion

    #region Private Methods



    #endregion
}
