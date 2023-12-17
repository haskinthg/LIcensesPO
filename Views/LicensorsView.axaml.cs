using Avalonia.Controls;
using LIcensesPO.Models;
using LIcensesPO.ViewModels;

namespace LIcensesPO.Views;

public partial class LicensorsView : Window
{
    public LicensorsView()
    {
        InitializeComponent();
        DataContext = new LicensorsViewModel();
    }
}