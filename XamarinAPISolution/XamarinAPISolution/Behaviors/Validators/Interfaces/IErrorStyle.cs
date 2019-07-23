using Xamarin.Forms;

namespace XamarinAPISolution.Behaviors.Validators.Interfaces
{
    public interface IErrorStyle
    {
        void ShowError(View view, string message);
        void RemoveError(View view);
    }
}
