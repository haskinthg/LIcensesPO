using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Views;

namespace LIcensesPO.ViewModels;

public class ComputersViewModel() : BaseTableViewModel<Computer, ComputersView>( new ComputerService());