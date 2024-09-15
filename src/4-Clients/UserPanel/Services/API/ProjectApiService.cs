using TaskoMask.BuildingBlocks.Contracts.Dtos.Projects;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Models;
using TaskoMask.BuildingBlocks.Contracts.ViewModels;
using TaskoMask.BuildingBlocks.Web.Services.Http;

namespace TaskoMask.Clients.UserPanel.Services.API;

public class ProjectApiService : BaseApiService
{
    #region Fields


    #endregion

    #region Ctor

    public ProjectApiService(IHttpClientService httpClientService)
        : base(httpClientService) { }

    #endregion

    #region Public Methods


    /// <summary>
    ///
    /// </summary>
    public async Task<Result<GetProjectDto>> GetAsync(string id)
    {
        var url = $"/owners-read/projects/{id}";
        return await HttpClientService.GetAsync<GetProjectDto>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<ProjectDetailsViewModel>> GetDetailsAsync(string id)
    {
        var url = $"/aggregator/projects/{id}";
        return await HttpClientService.GetAsync<ProjectDetailsViewModel>(url);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<IEnumerable<SelectListItem>>> GetSelectListItemsAsync(string organizationId)
    {
        var url = $"/owners-read/organizations/{organizationId}/projects";
        var projectsResult = await HttpClientService.GetAsync<IEnumerable<GetProjectDto>>(url);
        if (!projectsResult.IsSuccess)
            return Result.Failure<IEnumerable<SelectListItem>>(projectsResult.Errors, projectsResult.Message);

        var selectListItems = MapToSelectListItem(projectsResult.Value);
        return Result.Success(selectListItems);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> AddAsync(AddProjectDto input)
    {
        var url = $"/owners-write/projects";
        return await HttpClientService.PostAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> UpdateAsync(string id, UpdateProjectDto input)
    {
        var url = $"/owners-write/projects/{id}";
        return await HttpClientService.PutAsync<CommandResult>(url, input);
    }

    /// <summary>
    ///
    /// </summary>
    public async Task<Result<CommandResult>> DeleteAsync(string id)
    {
        var url = $"/owners-write/projects/{id}";
        return await HttpClientService.DeleteAsync<CommandResult>(url);
    }

    #endregion

    #region Private Methods



    /// <summary>
    ///
    /// </summary>
    private IEnumerable<SelectListItem> MapToSelectListItem(IEnumerable<GetProjectDto> projects)
    {
        var items = new List<SelectListItem>();
        foreach (var item in projects)
        {
            items.Add(new SelectListItem { Text = item.Name, Value = item.Id });
        }

        return items;
    }

    #endregion
}
