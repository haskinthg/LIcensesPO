using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LIcensesPO.DbConfig;
using LIcensesPO.ViewModels;
using LIcensesPO.Views;

namespace LIcensesPO;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var dataValidatorsPlugin = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();
        foreach (var plugin in dataValidatorsPlugin)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
        //init db
        AppDbContext db = new AppDbContext();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}