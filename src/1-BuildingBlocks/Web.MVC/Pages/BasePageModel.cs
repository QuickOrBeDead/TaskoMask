using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskoMask.BuildingBlocks.Application.Bus;

namespace TaskoMask.BuildingBlocks.Web.MVC.Pages;

public class BasePageModel : PageModel
{
    protected readonly IInMemoryBus InMemoryBus;

    public BasePageModel(IInMemoryBus inMemoryBus)
    {
        InMemoryBus = inMemoryBus;
    }
}
