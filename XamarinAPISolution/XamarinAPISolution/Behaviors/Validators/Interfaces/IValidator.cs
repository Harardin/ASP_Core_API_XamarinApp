namespace XamarinAPISolution.Behaviors.Validators.Interfaces
{
    public interface IValidator
    {
        string Message { get; }
        bool Check(string value);
    }
}
