using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AZEM.Controls
{
    /// <summary>
    /// Masked text field. Used with simple numeric masking requirements ie (###) ###-####
    /// </summary>
    public class MaskedEntry: Entry
    {
        private Regex regex;
        private bool isFormatting;

        public static readonly BindableProperty MaskPatternProperty =
            BindableProperty.Create("MaskPattern",
                            typeof(string),
                            typeof(MaskedEntry),
                            string.Empty);

        public string MaskPattern
        {
            get { return (string)this.GetValue(MaskPatternProperty); }
            set { this.SetValue(MaskPatternProperty, value); }
        }

        public MaskedEntry()
        {
            regex = new Regex("[^0-9]");
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == "Text" && !string.IsNullOrEmpty(MaskPattern))
            {
                if (this.isFormatting)
                    return;

                this.isFormatting = true;
                MaskedTextChanged();
                this.isFormatting = false;
            }
            base.OnPropertyChanged(propertyName);
        }

        public void MaskedTextChanged()
        {
            string result = regex.Replace(this.Text, "");
            if (!string.IsNullOrEmpty(result))
            {
                var resultArray = result.ToCharArray();
                var builder = new StringBuilder();
                var index = 0;
                foreach (var c in MaskPattern.ToCharArray())
                {
                    if (result.Length > index)
                    {
                        if (c == '#')
                        {
                            builder.Append(resultArray[index]);
                            index++;
                        }
                        else
                        {
                            builder.Append(c);
                        }
                    }

                }
                Text = builder.ToString();
            }
        }
    }
}
