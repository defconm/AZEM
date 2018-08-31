using AZEM.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AZEM.Behaviors
{
    class LoginBehaviors : Behavior<Entry>
    {
        public static readonly BindableProperty  IsRequiredProperty=
                BindableProperty.Create(
                   propertyName: "IsRequired",
                   returnType: typeof(String),
                   declaringType: typeof(EntryUnderlineControl),
                   defaultValue: false);
        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
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

        public static readonly BindableProperty IsValidProperty =
                BindableProperty.Create(
                    propertyName: "IsValid",
                    returnType: typeof(bool),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: true);
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public static readonly BindableProperty OnFocusColorProperty =
                BindableProperty.Create(
                    propertyName: "OnFocusColor",
                    returnType: typeof(Color),
                    declaringType: typeof(EntryUnderlineControl),
                    defaultValue: null);
        public Color OnFocusColor
        {
            get { return (Color)GetValue(OnFocusColorProperty); }
            set { SetValue(OnFocusColorProperty, value); }
        }

        void HandleOnFocus(object sender, FocusEventArgs e)
        {
            var entry = sender as EntryUnderlineControl;
            if (OnFocusColor != null)
                entry.EntryColor = OnFocusColor;
            IsValid = true;
        }

        void HandleUnFocus(object sender, FocusEventArgs e)
        {
            var entry = sender as EntryUnderlineControl;
            if (OnFocusColor != null)
                entry.EntryColor = Color.Black;
            if (!String.IsNullOrWhiteSpace(RegexString))
            {
                IsValid = Regex.IsMatch(entry.Text, RegexString);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Focused += HandleOnFocus;
            bindable.Unfocused += HandleUnFocus;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Focused -= HandleOnFocus;
            bindable.Unfocused -= HandleUnFocus;
        }
    }
}
