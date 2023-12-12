using Avalonia.Controls;
using LIcensesPO.ViewModels;

namespace LIcensesPO.Views;

public partial class LicensesView : Window
{
    public LicensesView()
    {
        InitializeComponent();
        DataContext = new LicensesViewModel();
    }
}