using Epsic.Authx.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Epsic.Authx.Data
{
    public class CovidDbContext : IdentityDbContext
    {
        public DbSet<TestCovid> TestsCovid { get; set; }

        public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options)
        {

        }
    }
}