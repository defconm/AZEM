using AZEM.Controls;
using Xamarin.Forms;

namespace AZEM.Pages
{
	public partial class LoginPage
	{
	    public LoginPage()
		{
		    InitializeComponent();

            var uxentryEntry = this.FindByName<EntryUnderlineControl>("EmailEntry");
            var uxpasswordEntry = this.FindByName<EntryUnderlineControl>("PasswordEntry");
            uxentryEntry.ReturnKeyType = (ReturnKeyTypes)4;
            uxentryEntry.NextFocus = ()=> uxpasswordEntry.Focus();
            uxpasswordEntry.NextFocus = () => uxpasswordEntry.Unfocus();
		}
    }
}
