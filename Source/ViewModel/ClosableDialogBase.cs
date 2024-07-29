using RDES.ConflictAnalyzer.DataModel;

namespace RDES.ConflictAnalyzer.ViewModel;

public abstract class ClosableDialogBase : NotifyBase
{
    public delegate void CloseDialogDelegate(bool? dialogResult);

    public event CloseDialogDelegate? CloseDialog;

    protected void SendDialogClose(bool? dialogResult)
    {
        CloseDialog?.Invoke(dialogResult);
    }
}