using Avalonia.Controls;
using LIcensesPO.ViewModels;

namespace LIcensesPO.Views;

public partial class ProgramsView : Window
{
    public ProgramsView()
    {
        InitializeComponent();
        DataContext = new ProgramsViewModel();
    }
}