using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
	public class ApplicationDbContext : ValsTechnologiesContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
			IConfigurationRoot configuration = new ConfigurationBuilder()
			   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			   .AddJsonFile("appsettings.json")
			   .Build();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServerDefault"));
			//base.OnConfiguring(optionsBuilder);
		}

	}
}
