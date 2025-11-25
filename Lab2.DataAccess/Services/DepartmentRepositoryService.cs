using Microsoft.EntityFrameworkCore;
using Lab2.DataAccess.Interfaces;

namespace Lab2.DataAccess.Services;

public class DepartmentRepositoryService : IDepartmentRepository
{
    private readonly DepartmentDbContext _db;

    public DepartmentRepositoryService(DepartmentDbContext db)
    {
        _db = db;
        _db.Database.EnsureCreated();
    }

    public IEnumerable<Department> GetAll()
    {
        return _db.Departments.ToList();
    }

    public int Add(Department department)
    {
        _db.Departments.Add(department);
        _db.SaveChanges();

        return department.Id;
    }

    public Department GetById(int departmentId)
    {
        var department = _db.Departments.FirstOrDefault(d => d.Id == departmentId);

        if (department == null)
        {
            throw new ArgumentException("Department not found.");
        }

        return department;
    }

    public Department Update(Department department)
    {
        var existingDepartment = _db.Departments.FirstOrDefault(d => d.Id == department.Id);

        if (existingDepartment == null)
        {
            throw new ArgumentException("Department not found.");
        }

        existingDepartment.Name = department.Name;
        existingDepartment.Code = department.Code;

        _db.SaveChanges();

        return existingDepartment;
    }

    public void Delete(int departmentId)
    {
        var existingDepartment = _db.Departments.FirstOrDefault(d => d.Id == departmentId);

        if (existingDepartment == null)
        {
            throw new ArgumentException("Department not found.");
        }

        _db.Departments.Remove(existingDepartment);
        _db.SaveChanges();
    }

    public IEnumerable<Employee> GetEmployees(int departmentId)
    {
        var department = _db.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == departmentId);

        if (department == null)
        {
            throw new ArgumentException("Department not found.");
        }

        return department.Employees.ToArray();
    }
}
