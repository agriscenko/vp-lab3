using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2.DataAccess;
using Lab2.DataAccess.Interfaces;
using Lab2.DataAccess.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lab3.WpfApplication.ViewModels;

class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
{
    private readonly DepartmentRepositoryService _repo;

    public MainWindowViewModel()
    {
        _repo = new DepartmentRepositoryService(new DepartmentDbContext());

        SelectDepartmentCommand = new RelayCommand(LoadEmployees);
        SearchCommand = new RelayCommand(FilterData);
        ResetFiltersCommand = new RelayCommand(ResetFilters);
        DeleteDepartmentCommand = new RelayCommand(DeleteSelectedDepartment);
        UpdateDepartmentCommand = new RelayCommand(OpenUpdateDepartmentWindow);
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
        _departments = _repo.GetAll().ToArray();
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

    public Department? SelectedDepartment { get; set; }

    public void LoadEmployees()
    {
        if (SelectedDepartment == null)
        {
            Employees = new List<Employee>();
            return;
        }

        Employees = _repo.GetEmployees(SelectedDepartment.Id).ToList();
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
        DepartmentFloors = _repo.GetAll()
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

    public enum HiringFilter { Any, Yes, No }

    private HiringFilter _hiringStatus;
    public HiringFilter HiringStatus
    {
        get => _hiringStatus;
        set
        {
            _hiringStatus = value;
            OnPropertyChanged();
        }
    }

    public void FilterData()
    {
        var query = _repo.GetAll().AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchDepartmentName))
        {
            query = query.Where(d => d.Name.Contains(SearchDepartmentName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (SelectedDepartmentFloor.HasValue)
        {
            query = query.Where(d => d.FloorNumber == SelectedDepartmentFloor.Value);
        }

        switch (HiringStatus)
        {
            case HiringFilter.Yes:
                query = query.Where(d => d.IsHiring);
                break;

            case HiringFilter.No:
                query = query.Where(d => !d.IsHiring);
                break;

            case HiringFilter.Any:
            default:
                break;
        }

        Departments = query.ToArray();
    }

    public void ResetFilters()
    {
        SearchDepartmentName = string.Empty;
        SelectedDepartmentFloor = null;
        HiringStatus = HiringFilter.Any;
        Employees = new List<Employee>();

        FilterData();
    }

    private void DeleteSelectedDepartment()
    {
        if (SelectedDepartment == null)
            return;

        var department = _repo.GetById(SelectedDepartment.Id);

        if (department != null)
        {
            _repo.Delete(department.Id);

            Departments = _repo.GetAll().ToArray();
        }
    }

    private void OpenUpdateDepartmentWindow()
    {
        if (SelectedDepartment == null)
            return;

        var updateWindow = new UpdateDepartmentWindow(SelectedDepartment);

        bool? result = updateWindow.ShowDialog();

        if (result == true)
        {
            _repo.Update(SelectedDepartment);

            Departments = _repo.GetAll().ToArray();
        }
    }

    public ICommand SelectDepartmentCommand { get; }

    public ICommand SearchCommand { get; }

    public ICommand ResetFiltersCommand { get; }

    public ICommand DeleteDepartmentCommand { get; }

    public ICommand UpdateDepartmentCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
