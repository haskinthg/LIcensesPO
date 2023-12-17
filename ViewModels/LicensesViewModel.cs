using System.Reactive;
using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Utils;
using LIcensesPO.Views;
using MsBox.Avalonia;
using ReactiveUI;

namespace LIcensesPO.ViewModels;

public class LicensesViewModel: BaseTableViewModel<License, LicensesView>
{
    public ReactiveCommand<long, Unit> DocxCommand { get; }

    public LicensesViewModel() : base(new LicenseService())
    {
        DocxCommand = ReactiveCommand.Create<long>(ExportDocx);
    }

    public void ExportDocx(long id)
    {
        License license = _service.GetById(id);
        string file = ExportLicenseWord.Export(license);
        MessageBoxManager.GetMessageBoxStandard("Экспорт WORD", $"Экспорт завершен: {file}").ShowAsync();
    }
}