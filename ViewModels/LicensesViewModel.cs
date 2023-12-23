using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Views;

namespace LIcensesPO.ViewModels;

public class LicensesViewModel(): BaseTableViewModel<License, LicensesView> (new LicenseService());