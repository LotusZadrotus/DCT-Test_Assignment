using System.ComponentModel;
using System.Runtime.CompilerServices;
using CryptoInfo.Annotations;

namespace CryptoInfo.ViewModels;

public class BaseViewModel: INotifyPropertyChanged, IViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Refresh()
    {
    }
}