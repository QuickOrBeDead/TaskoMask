﻿using TaskoMask.Application.Share.Dtos.Workspace.Cards;
using TaskoMask.Application.Share.Helpers;
using TaskoMask.Application.Share.ViewModels;
using TaskoMask.Presentation.Framework.Share.Contracts;
using TaskoMask.Presentation.Framework.Share.Helpers;
using TaskoMask.Presentation.Framework.Share.Services.Http;

namespace TaskoMask.Presentation.UI.UserPanel.Services.Data
{
    public class CardClientService : ICardClientService
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;

        #endregion

        #region Ctor

        public CardClientService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<CardBasicInfoDto>> Get(string id)
        {
            var uri = new ClientUriBuilder(new Uri(_httpClientService.GetBaseAddress(), $"/cards/{id}")).Uri;

            return await _httpClientService.GetAsync<CardBasicInfoDto>(uri);
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<IEnumerable<SelectListItem>>> GetSelectListItems(string boardId)
        {
            var uri = new ClientUriBuilder(new Uri(_httpClientService.GetBaseAddress(), $"/boards/{boardId}/cards")).Uri;
            return await _httpClientService.GetAsync<IEnumerable<SelectListItem>>(uri);
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<CommandResult>> Create(CardUpsertDto input)
        {
            var uri = new ClientUriBuilder(new Uri(_httpClientService.GetBaseAddress(), $"/cards")).Uri;
            return await _httpClientService.PostAsync<CommandResult>(uri, input);
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<CommandResult>> Update(string id,CardUpsertDto input)
        {
            var uri = new ClientUriBuilder(new Uri(_httpClientService.GetBaseAddress(), $"/cards/{id}")).Uri;
            return await _httpClientService.PutAsync<CommandResult>(uri, input);
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<CommandResult>> Delete(string id)
        {
            var uri = new ClientUriBuilder(new Uri(_httpClientService.GetBaseAddress(), $"/cards/{id}")).Uri;
            return await _httpClientService.DeleteAsync<CommandResult>(uri);
        }



        #endregion

        #region Private Methods



        #endregion

    }
}
