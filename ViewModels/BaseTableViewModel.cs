using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Collections;
using Avalonia.Controls;
using DynamicData;
using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Utils;
using LIcensesPO.Views;
using MsBox.Avalonia;
using ReactiveUI;

namespace LIcensesPO.ViewModels;

public class BaseTableViewModel<T, TView>: ViewModelBase where T: BaseEntity
{
    private BaseService<T> _service;
    public BaseTableViewModel(BaseService<T> service)
    {
        _service = service;
        
        _observableCollection.AddRange(_service.GetAll());
        // Инициализация списка лицензий
        Data = new DataGridCollectionView(_observableCollection);
        // Инициализация команд
        AddCommand = ReactiveCommand.Create(Add);
        DeleteCommand = ReactiveCommand.Create<long>(Delete);
        ExportCommand = ReactiveCommand.Create(Export);
        ExitCommand = ReactiveCommand.Create(Exit);
        ChangeTableCommand = ReactiveCommand.Create<String>(ChangeTable);
    }
    
    private DataGridCollectionView _licenses;

    public DataGridCollectionView Data
    {
        get => _licenses;
        set => this.RaiseAndSetIfChanged(ref _licenses, value);
    }
    
    ObservableCollection<T> _observableCollection = new();

    public ReactiveCommand<long, Unit> DeleteCommand { get; }
    
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    
    public ReactiveCommand<Unit, Unit> ExportCommand { get; }
    
    public ReactiveCommand<Unit, Unit> ExitCommand { get; }
    
    public ReactiveCommand<String, Unit> ChangeTableCommand { get; }
    

    private void Exit()
    {
        var loginWin = new MainWindow();
        loginWin.Show();
        WindowUtils.SetMainWindow(loginWin);
        WindowUtils.CloseWindow<LicensesView>();
    }

    private void ChangeTable(String type)
    {
        Window window = null;
        switch (type)
        {
            case "Licenses":
                window = new LicensesView();
                break;
            case "Computers":
                window = new ComputersView();
                break;
            case "Licensors":
                window = new LicensorsView();
                break;
            case "Programs":
                window = new ProgramsView();
                break;
        }
        
        window.Show();
        WindowUtils.SetMainWindow(window);
        WindowUtils.CloseWindow<TView>();
    }

    private void Refresh()
    {
        _observableCollection.Clear();
        _observableCollection.AddRange(_service.GetAll());
        Data.Refresh();
    }

    private async void Add()
    {
        var obj = new ObjectEditorWindow<T>();
        T result = await obj.ShowDialog<T>(WindowUtils.GetCurrent<TView>());
        result.Id = null;
        _service.Add(result);
        Refresh();
    }
    

    private void Delete(long id)
    {
        Data.Remove(_service.GetById(id));
        _service.Delete(id);
        Refresh();
    }

    private void Export()
    {
        String file = ExportXlsx.Export(_observableCollection);
        MessageBoxManager.GetMessageBoxStandard("Экспорт XLSX", $"Экспорт завершен: {file}").ShowAsync();
    }
}