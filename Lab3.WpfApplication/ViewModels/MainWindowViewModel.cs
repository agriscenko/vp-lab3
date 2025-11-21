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
        SearchCommand = new RelayCommand(FilterData);
        ResetFiltersCommand = new RelayCommand(ResetFilters);
    }

    private Department[] _departments;

    public Department[] Departments
    {
        get => _departments;
        private set
        {
            _departments = value;
            OnPropertyChanged();
        }
    }

    public void Load()
    {
        _departments = _db.Departments
            .Where(d => d.IsActive)
            .ToArray();
        LoadDepartmentFloors();
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
            Employees = [];
            return;
        }

        Employees = _db.Employees.Where(e => e.Department.Id == SelectedDepartment.Id).ToList();
    }

    private List<int> _departmentFloors;

    public List<int> DepartmentFloors
    {
        get => _departmentFloors;
        private set
        {
            _departmentFloors = value;
            OnPropertyChanged();
        }
    }

    private int? _selectedDepartmentFloor;

    public int? SelectedDepartmentFloor
    {
        get => _selectedDepartmentFloor;
        set
        {
            _selectedDepartmentFloor = value;
            OnPropertyChanged();
        }
    }

    private void LoadDepartmentFloors()
    {
        DepartmentFloors = _db.Departments
            .Select(d => d.FloorNumber)
            .Distinct()
            .OrderBy(f => f)
            .ToList();

        SelectedDepartmentFloor = null;
    }

    private string _searchDepartmentName;

    public string SearchDepartmentName
    {
        get => _searchDepartmentName;
        set
        {
            _searchDepartmentName = value;
            OnPropertyChanged();
        }
    }

    private bool _showInactiveDepartments;

    public bool ShowInactiveDepartments
    {
        get => _showInactiveDepartments;
        set
        {
            if (_showInactiveDepartments != value)
            {
                _showInactiveDepartments = value;
                OnPropertyChanged();
            }
        }
    }

    public void FilterData()
    {
        var query = _db.Departments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchDepartmentName))
        {
            query = query.Where(d => d.Name.Contains(SearchDepartmentName));
        }

        if (SelectedDepartmentFloor.HasValue)
        {
            query = query.Where(d => d.FloorNumber == SelectedDepartmentFloor.Value);
        }

        if (ShowInactiveDepartments)
        {
            query = query.Where(d => !d.IsActive);
        }

        Departments = query.ToArray();
    }

    public void ResetFilters()
    {
        SearchDepartmentName = string.Empty;
        SelectedDepartmentFloor = null;
        ShowInactiveDepartments = false;
        Employees = [];

        FilterData();
    }

    public ICommand SelectDepartmentCommand { get; }

    public ICommand SearchCommand { get; }

    public ICommand ResetFiltersCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
