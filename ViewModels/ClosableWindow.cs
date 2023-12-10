using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace LIcensesPO.ViewModels;

public static class WindowUtils
{
    public static void SetMainWindow(Window window)
    {
        var lifeTime = (IClassicDesktopStyleApplicationLifetime)Application.Current?.ApplicationLifetime!;
        lifeTime.MainWindow = window;
    }

    public static void CloseWindow<TWindow>()
    {
        var lifeTime = (IClassicDesktopStyleApplicationLifetime)Application.Current?.ApplicationLifetime!;
        lifeTime?.Windows.Last(w => w is TWindow).Close();
    }
}