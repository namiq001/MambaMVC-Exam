using MambaMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MambaMVC.MambaDataContext;

public class MambaDbContext : IdentityDbContext<AppUser>
{
	public MambaDbContext(DbContextOptions<MambaDbContext> options ) : base(options)
	{

	}
	public DbSet<Team> Teams { get; set; }
	public DbSet<WorkType> WorkTypes { get; set; }
	public DbSet<Setting> Settings { get; set; }
}
