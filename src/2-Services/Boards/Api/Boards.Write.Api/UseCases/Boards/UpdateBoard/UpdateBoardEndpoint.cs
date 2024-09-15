using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Boards;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Services;
using TaskoMask.BuildingBlocks.Web.MVC.Controllers;

namespace TaskoMask.Services.Boards.Write.Api.UseCases.Boards.UpdateBoard;

[Authorize("user-write-access")]
[Tags("Boards")]
public class UpdateBoardEndpoint : BaseApiController
{
    public UpdateBoardEndpoint(IAuthenticatedUserService authenticatedUserService, IInMemoryBus inMemoryBus)
        : base(authenticatedUserService, inMemoryBus) { }

    /// <summary>
    /// Update an existing board
    /// </summary>
    [HttpPut]
    [Route("boards/{id}")]
    public async Task<Result<CommandResult>> Put(string id, [FromBody] UpdateBoardDto input)
    {
        return await InMemoryBus.SendCommand<UpdateBoardRequest>(new(id: id, name: input.Name, description: input.Description));
    }
}
