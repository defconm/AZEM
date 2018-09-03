using AZEM.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AZEM
{
	public partial class LoginPage : ContentPage
	{
        EntryUnderlineControl _UxentryEntry;
        EntryUnderlineControl _UxpasswordEntry;
		public LoginPage()
		{
			InitializeComponent();
            _UxentryEntry = this.FindByName<EntryUnderlineControl>("EmailEntry");
            _UxpasswordEntry = this.FindByName<EntryUnderlineControl>("PasswordEntry");
            _UxentryEntry.ReturnKeyType = (ReturnKeyTypes)4;
            _UxentryEntry.NextFocus = ()=> _UxpasswordEntry.Focus();
            _UxpasswordEntry.NextFocus = () => _UxpasswordEntry.Unfocus();
		}
	}
}
