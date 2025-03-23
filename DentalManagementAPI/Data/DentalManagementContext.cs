using Microsoft.EntityFrameworkCore;
using DentalManagementAPI.Models;

namespace DentalManagementAPI.Data
{
    public class DentalManagementContext : DbContext
    {
        public DentalManagementContext(DbContextOptions<DentalManagementContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
    }
}
