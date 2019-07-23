using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinAPISolution.Behaviors.Validators.Functions;
using XamarinAPISolution.Behaviors.Validators.Interfaces;

namespace XamarinAPISolution.Behaviors
{
    public class ValidationBehavior : Behavior<View>
    {
        private IErrorStyle style = new BasicErrorStyle();
        private View view;
        
        public string PropertyType { get; set; }
        public ValidationGroupBehavior Group { get; set; }
        public ObservableCollection<IValidator> Validators { get; set; } = new ObservableCollection<IValidator>();

        public bool Validate()
        {
            bool isValid = true;
            string errorMessage = string.Empty;

            foreach (IValidator validator in Validators)
            {
                bool result = validator.Check(view.GetType()
                    .GetProperty(PropertyType)
                    .GetValue(view) as string);

                isValid = isValid && result;

                if(!result)
                {
                    errorMessage = validator.Message;
                    break;
                }
            }
            if (!isValid)
            {
                style.ShowError(view, errorMessage);
                return false;
            }
            else
            {
                style.RemoveError(view);
                return true;
            }
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            view = bindable as View;
            view.PropertyChanged += OnPropertyChanged;
            view.Unfocused += OnUnFocused;

            if (Group != null)
            {
                Group.Add(this);
            }
        }
        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);

            view.PropertyChanged -= OnPropertyChanged;
            view.Unfocused -= OnUnFocused;

            if (Group != null)
            {
                Group.Remove(this);
            }
        }
        void OnUnFocused(object sender, FocusEventArgs e)
        {
            Validate();

            if (Group != null)
            {
                Group.Update();
            }
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyType)
            {
                Validate();
            }
        }
    }
}
