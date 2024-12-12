using Microsoft.EntityFrameworkCore;
using SecondASPcorePractice.Models.Entities;

namespace SecondASPcorePractice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<StudentEnroll> studentEnrollment {get; set;}

    }
}
