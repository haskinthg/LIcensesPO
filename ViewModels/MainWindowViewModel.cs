using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using LIcensesPO.DbConfig;
using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Views;
using ReactiveUI;

namespace LIcensesPO.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private User _user;
    private bool _switchFlag = false;
    private string _switchContent = "Регистрация";
    private string _submitContent = "Войти";
    private readonly AuthService _authService;

    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    public bool SwitchFlag
    {
        get => _switchFlag;
        set => this.RaiseAndSetIfChanged(ref _switchFlag, value);
    }

    public string SwitchContent { 
        get => _switchContent;
        set => this.RaiseAndSetIfChanged(ref _switchContent, value);
    }
    
    public string SubmitContent { 
        get => _submitContent;
        set => this.RaiseAndSetIfChanged(ref _submitContent, value);
    }

    public ReactiveCommand<Unit, Unit> SwitchCommand { get; }
    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    public MainWindowViewModel()
    {
        User = new User();
        _authService = new AuthService();
        SwitchCommand = ReactiveCommand.Create(Switch);
        SubmitCommand = ReactiveCommand.Create(Submit);

        // Инициализация начального состояния представления
        SwitchFlag = false;
        _setStateButtons();
    }

    private void Submit()
    {
        _setStateButtons();
        if (SwitchFlag)
        {
            _authService.Register(_user);
            Switch();
        }
        else
        {
            bool isAuth = _authService.Login(_user.Login, _user.Password);
            if (isAuth)
            {
                var licWindow = new LicensesView();
                licWindow.Show();
                WindowUtils.SetMainWindow(licWindow);
                WindowUtils.CloseWindow<MainWindow>();
            }
        }
    }

    private void Switch()
    {
        _setStateButtons();
        SwitchFlag = !SwitchFlag;
    }

    private void _setStateButtons()
    {
        SwitchContent = SwitchFlag ? "Регистрация" : "Вход";
        SubmitContent = SwitchFlag ? "Войти" : "Зарегистрироваться";
    }
}