using Microsoft.EntityFrameworkCore;

namespace Lab2.DataAccess;

public class DepartmentDbContext : DbContext
{
    private readonly static string ConnectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Documents\\Work\\Lab3\\Lab2.DataAccess\\DepartmentDb.mdf;Integrated Security=True";

    public DbSet<Department> Departments { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    public DepartmentDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConnectionString);
}
