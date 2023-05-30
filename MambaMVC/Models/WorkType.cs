namespace MambaMVC.Models;

public class WorkType
{
    public int Id { get; set; }
    public string WorkTypeName { get; set; } = null!;
    public List<Team> Teams { get; set; }
}
