using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers.Data
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions options): base(options) 
		{
		}
		
	}
}
