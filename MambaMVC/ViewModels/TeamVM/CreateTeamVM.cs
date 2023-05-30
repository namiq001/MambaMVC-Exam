using MambaMVC.Models;

namespace MambaMVC.ViewModels.TeamVM;

public class CreateTeamVM
{

    public string Name { get; set; } = null!;
    public int WorkTypeId { get; set; }
    public IFormFile Image { get; set; } = null!;
    public List<WorkType>? WorkTypes { get; set; }
}
