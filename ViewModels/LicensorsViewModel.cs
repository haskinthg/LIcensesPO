using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Views;

namespace LIcensesPO.ViewModels;

public class LicensorsViewModel() : BaseTableViewModel<Licensor, LicensorsView>( new LicensorService());