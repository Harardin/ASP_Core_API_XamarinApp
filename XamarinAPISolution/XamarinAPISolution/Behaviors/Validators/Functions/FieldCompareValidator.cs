using XamarinAPISolution.Behaviors.Validators.Interfaces;

namespace XamarinAPISolution.Behaviors.Validators.Functions
{
    public class FieldCompareValidator : IValidator
    {
        public string Message { get; set; } = "Fields fo not do not match";
        public string Field { get; set; }
        public bool Check(string value)
        {
            if(value == Field)
            {
                System.Diagnostics.Debug.WriteLine("Field from ConfPassword = " + value + "Field from Password = " + Field);
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Field from ConfPassword = " + value + "Field from Password = " + Field);
                return false;
            }
        }
    }
}
