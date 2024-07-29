using System.Windows.Input;

namespace RDES.ConflictAnalyzer.ViewModel;

public class RelayCommand : ICommand
{
    readonly Action<object?> _execute;
    readonly Func<object?, bool>? _canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Initializes a new instance of RelayCommand
    /// </summary>
    /// <param name="execute">Delegate to execute when Execute is called on the command.</param>
    /// <param name="canExecute">Optional delegate to be called to determine can execute</param>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    /// <summary>
    /// Can execute implementation for ICommand
    /// </summary>
    /// <param name="parameter">The command parameter</param>
    /// <returns></returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    /// Execute implementation for ICommand
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}