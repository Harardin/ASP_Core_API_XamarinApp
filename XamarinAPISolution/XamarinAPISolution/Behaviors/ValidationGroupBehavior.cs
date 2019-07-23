using Xamarin.Forms;
using System.Collections.Generic;

namespace XamarinAPISolution.Behaviors
{
    public class ValidationGroupBehavior : Behavior<View>
    {
        IList<ValidationBehavior> validationBehaviors;
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create("IsValid", typeof(bool), typeof(ValidationGroupBehavior), false);

        public ValidationGroupBehavior()
        {
            validationBehaviors = new List<ValidationBehavior>();
        }
        public void Add(ValidationBehavior _validationBehavior)
        {
            validationBehaviors.Add(_validationBehavior);
        }
        public void Remove(ValidationBehavior _validationBehavior)
        {
            validationBehaviors.Remove(_validationBehavior);
        }

        public void Update()
        {
            bool isValid = true;

            foreach(ValidationBehavior validation in validationBehaviors)
            {
                isValid = isValid && validation.Validate();
            }

            IsValid = isValid;
        }

        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            set
            {
                SetValue(IsValidProperty, value);
            }
        }
    }
}
