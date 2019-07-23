using XamarinAPISolution.Behaviors.Validators.Interfaces;

namespace XamarinAPISolution.Behaviors.Validators.Functions
{
    public class RequiredFieldValidator : IValidator
    {
        public string Message { get; set; } = "This field is required";

        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
