using LIcensesPO.Models;
using LIcensesPO.Services;
using LIcensesPO.Views;

namespace LIcensesPO.ViewModels;

public class ProgramsViewModel() : BaseTableViewModel<Prog, ProgramsView>( new ProgService());