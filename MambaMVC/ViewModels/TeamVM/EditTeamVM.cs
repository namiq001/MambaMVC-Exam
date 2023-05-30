using MambaMVC.Models;

namespace MambaMVC.ViewModels.TeamVM;

public class EditTeamVM
{
    public string? Name { get; set; }
    public int WorkTypeId { get; set; }
    public string? ImageName { get; set; }
    public IFormFile? Image { get; set; }
    public List<WorkType>? WorkTypes { get; set; }
}
