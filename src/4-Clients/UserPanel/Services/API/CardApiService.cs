﻿using TaskoMask.BuildingBlocks.Contracts.Dtos.Cards;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Models;
using TaskoMask.BuildingBlocks.Web.Services.Http;

namespace TaskoMask.Clients.UserPanel.Services.API;

public class CardApiService : BaseApiService
{
    #region Fields


    #endregion

    #region Ctor

    public CardApiService(IHttpClientService httpClientService)
        : base(httpClientService) { }

    #endregion

    #region Public Methods


    /// <summary>
    ///
    /// </summary>
    public async Task<Result<GetCardDto>> GetAsync(string id)
    {
        var url = $"/cards/{id}";
        return await HttpClientService.GetAsync<GetCardDto>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<IEnumerable<SelectListItem>>> GetSelectListItemsAsync(string boardId)
    {
        var url = $"/boards/{boardId}/cards";
        return await HttpClientService.GetAsync<IEnumerable<SelectListItem>>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> AddAsync(AddCardDto input)
    {
        var url = $"/cards";
        return await HttpClientService.PostAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> UpdateAsync(string id, UpdateCardDto input)
    {
        var url = $"/cards/{id}";
        return await HttpClientService.PutAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> DeleteAsync(string id)
    {
        var url = $"/cards/{id}";
        return await HttpClientService.DeleteAsync<CommandResult>(url);
    }

    #endregion

    #region Private Methods



    #endregion
}
