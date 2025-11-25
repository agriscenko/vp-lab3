using Lab3.WpfApplication.ViewModels;
using Lab2.DataAccess;
using System.Windows;

namespace Lab3.WpfApplication;

/// <summary>
/// Interaction logic for UpdateDepartmentWindow.xaml
/// </summary>
public partial class UpdateDepartmentWindow : Window
{
    public UpdateDepartmentWindow(Department department)
    {
        InitializeComponent();

        var viewModel = new UpdateDepartmentViewModel(department);

        viewModel.RequestClose += () =>
        {
            DialogResult = true;
            Close();
        };

        DataContext = viewModel;
    }
}
