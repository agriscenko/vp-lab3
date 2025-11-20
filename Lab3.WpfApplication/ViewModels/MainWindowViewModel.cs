using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lab3.WpfApplication.ViewModels;

class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
{
    private DepartmentDbContext _db;

    public MainWindowViewModel()
    {
        _db = new DepartmentDbContext();
        SelectDepartmentCommand = new RelayCommand(LoadEmployees);
    }

    private Department[] _departments;

    public Department[] Departments => _departments;

    public void Load()
    {
        _departments = _db.Departments.ToArray();
    }

    private List<Employee> _employees = new List<Employee>();

    public List<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged();
        }
    }

    public Department SelectedDepartment { get; set; }

    public void LoadEmployees()
    {
        if (SelectedDepartment == null)
        {
            return;
        }

        Employees = _db.Employees.Where(e => e.Department.Id == SelectedDepartment.Id).ToList();
    }

    public ICommand SelectDepartmentCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
