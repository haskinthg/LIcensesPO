using System;
using System.Reactive;
using System.Reactive.Linq;
using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Views;
using MsBox.Avalonia;
using ReactiveUI;

namespace LIcensesPO.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _password;
    private string _login;
    private string _lname;
    private string _fname;
    private bool _switchFlag = true;
    private string _switchContent;
    private string _submitContent;
    private readonly AuthService _authService;
    public String Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    
    public String Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public String LName
    {
        get => _lname;
        set => this.RaiseAndSetIfChanged(ref _lname, value);
    }
    
    public String FName
    {
        get => _fname;
        set => this.RaiseAndSetIfChanged(ref _fname, value);
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
        _authService = new AuthService();

        var canExecute = this.WhenAnyValue(
            x => x.Login,
            x => x.Password,
            x => x.LName,
            x => x.FName,
            (l, p, ln, lf) =>
            {
                bool authCheck = !string.IsNullOrWhiteSpace(l) && !string.IsNullOrWhiteSpace(p);
                if (!SwitchFlag)
                    return authCheck;
                else return authCheck && !string.IsNullOrWhiteSpace(ln) && !string.IsNullOrWhiteSpace(lf);
            });
        
        SwitchCommand = ReactiveCommand.Create(Switch);
        SubmitCommand = ReactiveCommand.Create(Submit, canExecute);

        // Инициализация начального состояния представления
        SwitchFlag = false;
        _setStateButtons();
    }

    private void Submit()
    {
        var _user = new User()
        {
            Login = Login,
            Password = Password,
            LName = LName,
            FName = FName
        };
        _setStateButtons();
        if (SwitchFlag)
        {
            _authService.Register(_user);
            Switch();
        }
        else
        {
            try
            {
                bool isAuth = _authService.Login(_user.Login, _user.Password);
                if (!isAuth) return;
                var window = new ComputersView();
                window.Show();
                WindowUtils.SetMainWindow(window);
                WindowUtils.CloseWindow<MainWindow>();
            }
            catch (Exception e)
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка", e.Message).ShowAsync();
            }
        }
    }

    private void Switch()
    {
        SwitchFlag = !SwitchFlag;
        _setStateButtons();
    }

    private void _setStateButtons()
    {
        SwitchContent = !SwitchFlag ? "Регистрация" : "Вход";
        SubmitContent = !SwitchFlag ? "Войти" : "Зарегистрироваться";
    }
}