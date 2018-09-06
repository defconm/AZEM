using AZEM.Controls;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AZEM.Behaviors
{
    class LoginBehaviors : Behavior<EntryUnderlineControl>
    {
        public static readonly BindableProperty  IsRequiredProperty=
                BindableProperty.Create(
                   "IsRequired",
                   typeof(bool),
                   typeof(EntryUnderlineControl),
                   false);
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }
        public static readonly BindableProperty IsPasswordProperty=
                BindableProperty.Create(
                    "IsPassword",
                    typeof(bool),
                    typeof(EntryUnderlineControl),
                    false);
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        public static readonly BindableProperty IsEmailProperty =
                BindableProperty.Create(
                    "IsEmail",
                    typeof(bool),
                    typeof(EntryUnderlineControl),
                    false);
        public bool IsEmail
        {
            get => (bool)GetValue(IsEmailProperty);
            set => SetValue(IsEmailProperty, value);
        }
        public static readonly BindableProperty ConfirmPasswordTextProperty =
                BindableProperty.Create(
                    "ConfirmPasswordText",
                    typeof(string),
                    typeof(EntryUnderlineControl));
        public string ConfirmPasswordText
        {
            get => (string)GetValue(ConfirmPasswordTextProperty);
            set => SetValue(ConfirmPasswordTextProperty, value);
        }
        public static readonly BindableProperty RegexProperty =
                BindableProperty.Create(
                    "RegexString",
                    typeof(string),
                    typeof(EntryUnderlineControl));
        public string RegexString
        {
            get => (string)GetValue(RegexProperty);
            set => SetValue(RegexProperty, value);
        }

        public static readonly BindableProperty ErrorMessageProperty =
                BindableProperty.Create(
                    "ErrorMessage",
                    typeof(string),
                    typeof(EntryUnderlineControl));
        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        public static readonly BindableProperty IsNotValidProperty =
                BindableProperty.Create(
                    "IsNotValid",
                    typeof(bool),
                    typeof(EntryUnderlineControl),
                    false);
        public bool IsNotValid
        {
            get => (bool)GetValue(IsNotValidProperty);
            set => SetValue(IsNotValidProperty, value);
        }

        public static readonly BindableProperty OnFocusColorProperty =
                BindableProperty.Create(
                    "OnFocusColor",
                    typeof(Color),
                    typeof(EntryUnderlineControl),
                    Color.Default);
        public Color OnFocusColor
        {
            get => (Color)GetValue(OnFocusColorProperty);
            set => SetValue(OnFocusColorProperty, value);
        }

        void HandleOnFocus(object sender, FocusEventArgs e)
        {
            var entry = (EntryUnderlineControl)sender;
            entry.HasError = false;
            entry.EntryColor = OnFocusColor;
            IsNotValid = false;
        }

        void HandleUnFocusEmail(object sender, FocusEventArgs e)
        {
            var entry = (EntryUnderlineControl)sender;
            entry.EntryColor = Color.Black;
            if (string.IsNullOrWhiteSpace(entry.Text))
            {
                if (!IsEmail && !IsPassword)
                    ErrorMessage = ErrorMessage;
                if(IsEmail)
                    ErrorMessage = "Email address is required";
                if (IsPassword)
                    ErrorMessage = "Password is required";
                entry.HasError = true;
                IsNotValid = true;
                return;
            }
            entry.Text = entry.Text.Trim();
            if (!string.IsNullOrWhiteSpace(RegexString))
            {
                IsNotValid = !(Regex.IsMatch(entry.Text, RegexString));
                entry.HasError = IsNotValid;
                if(IsEmail)
                    ErrorMessage = "Invalid email address";
                if (IsPassword)
                    ErrorMessage = "Invalid password";
                return;
            }
            entry.HasError = false;
        }

        protected override void OnAttachedTo(EntryUnderlineControl bindable)
        {
            bindable.Focused += HandleOnFocus;
            bindable.Unfocused += HandleUnFocusEmail;
        }

        protected override void OnDetachingFrom(EntryUnderlineControl bindable)
        {
            bindable.Focused -= HandleOnFocus;
            bindable.Unfocused -= HandleUnFocusEmail;

        }
    }
}
