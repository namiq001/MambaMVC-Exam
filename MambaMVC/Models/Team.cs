namespace MambaMVC.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProfilImage { get; set; } = null!;
    public int WorkTypeId { get; set; }
    public WorkType WorkTypes { get; set; }
}
