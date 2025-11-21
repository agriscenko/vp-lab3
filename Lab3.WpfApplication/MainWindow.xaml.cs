using Lab3.WpfApplication.ViewModels;
using System.Windows;

namespace Lab3.WpfApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;
    
    public MainWindow()
    {
        InitializeComponent();
        InitModel();
    }

    private void InitModel()
    {
        _viewModel = new MainWindowViewModel();

        _viewModel.Load();

        this.DataContext = _viewModel;
    }
}
