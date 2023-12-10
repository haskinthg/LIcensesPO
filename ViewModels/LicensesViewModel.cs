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

    public ReactiveCommand<License, Unit> DeleteCommand { get; }

    public LicensesViewModel()
    {
        _licenseService = new LicenseService();
        // Инициализация списка лицензий
        Licenses = new DataGridCollectionView(_licenseService.GetAll());
        // Инициализация команды удаления
        DeleteCommand = ReactiveCommand.CreateFromObservable<License, Unit>(license =>
        {
            Licenses.Remove(license);
            return Observable.Return(Unit.Default);
        });
    }
}