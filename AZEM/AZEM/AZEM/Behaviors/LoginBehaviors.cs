using AZEM.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AZEM.Behaviors
{
    class LoginBehaviors : Behavior<EntryUnderlineControl>
    {
        public static readonly BindableProperty  IsRequiredProperty=
                BindableProperty.Create(
                   propertyName: "IsRequired",
                   returnType: typeof(bool),
                   declaringType: typeof(EntryUnderlineControl),
                   defaultValue: false);
        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }
        public static readonly BindableProperty IsPasswordProperty=
                BindableProperty.Create(
                    propertyName: "IsPassword",
                    returnType: typeof(bool),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: false);
        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
        public static readonly BindableProperty IsEmailProperty =
                BindableProperty.Create(
                    propertyName: "IsEmail",
                    returnType: typeof(bool),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: false);
        public bool IsEmail
        {
            get { return (bool)GetValue(IsEmailProperty); }
            set { SetValue(IsEmailProperty, value); }
        }
        public static readonly BindableProperty RegexProperty =
                BindableProperty.Create(
                    propertyName: "RegexString",
                    returnType: typeof(string),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: null);
        public string RegexString
        {
            get { return (string)GetValue(RegexProperty); }
            set { SetValue(RegexProperty, value); }
        }

        public static readonly BindableProperty ErrorMessageProperty =
                BindableProperty.Create(
                    propertyName: "ErrorMessage",
                    returnType: typeof(string),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: null);
        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public static readonly BindableProperty IsNotValidProperty =
                BindableProperty.Create(
                    propertyName: "IsNotValid",
                    returnType: typeof(bool),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: false);
        public bool IsNotValid
        {
            get { return (bool)GetValue(IsNotValidProperty); }
            set { SetValue(IsNotValidProperty, value); }
        }

        public static readonly BindableProperty OnFocusColorProperty =
                BindableProperty.Create(
                    propertyName: "OnFocusColor",
                    returnType: typeof(Color),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: Color.Default);
        public Color OnFocusColor
        {
            get { return (Color)GetValue(OnFocusColorProperty); }
            set { SetValue(OnFocusColorProperty, value); }
        }

        void HandleOnFocus(object sender, FocusEventArgs e)
        {
            var entry = sender as EntryUnderlineControl;
            entry.HasError = false;
            if (OnFocusColor != null)
                entry.EntryColor = OnFocusColor;
            IsNotValid = false;
        }

        void HandleUnFocusEmail(object sender, FocusEventArgs e)
        {
            var entry = sender as EntryUnderlineControl;
            entry.Text = entry.Text.Trim();
            if (OnFocusColor != null)
                entry.EntryColor = Color.Black;
            if (String.IsNullOrWhiteSpace(entry.Text))
            {
                if(IsEmail)
                    ErrorMessage = "Email address is required";
                if (IsPassword)
                    ErrorMessage = "Password is required";
                entry.HasError = true;
                IsNotValid = true;
                return;
            }
            if (!String.IsNullOrWhiteSpace(RegexString))
            {
                IsNotValid = !(Regex.IsMatch(entry.Text, RegexString));
                entry.HasError = IsNotValid;
                if(IsEmail)
                    ErrorMessage = "Invalid email address";
                if (IsPassword)
                    ErrorMessage = "Invalid password";
            }
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
