using Avalonia.Controls;
using LIcensesPO.ViewModels;

namespace LIcensesPO.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}