using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2.DataAccess;
using System.Windows.Input;

namespace Lab3.WpfApplication.ViewModels;

public partial class UpdateDepartmentViewModel : ObservableObject
{
    public Department Department { get; }

    public UpdateDepartmentViewModel(Department department)
    {
        Department = department;
        SaveCommand = new RelayCommand(Save);
    }

    public event Action? RequestClose;

    public void Save()
    {
        RequestClose?.Invoke();
    }

    public ICommand SaveCommand { get; }
}
