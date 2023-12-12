using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Collections;
using LIcensesPO.Models;
using LIcensesPO.Services;
using ReactiveUI;

namespace LIcensesPO.ViewModels;

public class LicensesViewModel: ViewModelBase
{
    private LicenseService _licenseService;
    private DataGridCollectionView _licenses;

    public DataGridCollectionView Licenses
    {
        get => _licenses;
        set => this.RaiseAndSetIfChanged(ref _licenses, value);
    }

    public ReactiveCommand<int, Unit> DeleteCommand { get; }
    
    public ReactiveCommand<Unit, Unit> AddCommand { get; }

    public LicensesViewModel()
    {
        _licenseService = new LicenseService();
        // Инициализация списка лицензий
        Licenses = new DataGridCollectionView(_licenseService.GetAll());
        // Инициализация команд
        AddCommand = ReactiveCommand.Create(Add);
        DeleteCommand = ReactiveCommand.Create<int>(Delete);
    }

    private void Add()
    {
        Licenses.AddNew();
 //       Licenses.CommitNew();
    }

    private void Delete(int id)
    {
        Licenses.Remove(_licenseService.GetById(id));
        _licenseService.Delete(id);
    }
}