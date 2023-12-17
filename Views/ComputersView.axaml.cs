using Avalonia.Controls;
using LIcensesPO.ViewModels;

namespace LIcensesPO.Views;

public partial class ComputersView : Window
{
    public ComputersView()
    {
        InitializeComponent();
        DataContext = new ComputersViewModel();
    }
}