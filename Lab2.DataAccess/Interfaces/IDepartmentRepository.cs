namespace Lab2.DataAccess.Interfaces;

public interface IDepartmentRepository
{
    IEnumerable<Department> GetAll();

    int Add(Department department);

    Department GetById(int departmentId);

    Department Update(Department department);

    void Delete(int departmentId);

    IEnumerable<Employee> GetEmployees(int departmentId);
}
