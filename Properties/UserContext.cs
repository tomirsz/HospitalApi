using Microsoft.EntityFrameworkCore;
using HospitalApiModels;

public class UserContext :DbContext
{

    private readonly string connectionString;

    public UserContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Nurse> Nurse { get; set; }
    public DbSet<Duty> Duty { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
}
