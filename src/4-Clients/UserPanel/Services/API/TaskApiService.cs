﻿using TaskoMask.BuildingBlocks.Contracts.Dtos.Tasks;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.ViewModels;
using TaskoMask.BuildingBlocks.Web.Services.Http;

namespace TaskoMask.Clients.UserPanel.Services.API;

public class TaskApiService : BaseApiService
{
    #region Fields


    #endregion

    #region Ctor

    public TaskApiService(IHttpClientService httpClientService)
        : base(httpClientService) { }

    #endregion

    #region Public Methods


    /// <summary>
    ///
    /// </summary>
    public async Task<Result<GetTaskDto>> GetAsync(string id)
    {
        var url = $"/tasks/{id}";
        return await HttpClientService.GetAsync<GetTaskDto>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<TaskDetailsViewModel>> GetDetailsAsync(string id)
    {
        var url = $"/tasks/{id}/details";
        return await HttpClientService.GetAsync<TaskDetailsViewModel>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> AddAsync(AddTaskDto input)
    {
        var url = $"/tasks";
        return await HttpClientService.PostAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> UpdateAsync(string id, UpdateTaskDto input)
    {
        var url = $"/tasks/{id}";
        return await HttpClientService.PutAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> MoveTaskToAnotherCardAsync(string taskId, string cardId)
    {
        var url = $"/tasks/{taskId}/moveto/{cardId}";
        return await HttpClientService.PutAsync<CommandResult>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> DeleteAsync(string id)
    {
        var url = $"/tasks/{id}";
        return await HttpClientService.DeleteAsync<CommandResult>(url);
    }

    #endregion

    #region Private Methods



    #endregion
}
