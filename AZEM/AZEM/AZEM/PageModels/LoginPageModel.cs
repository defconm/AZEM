using System;
using AZEM.Extensions;
using AZEM.Model;

using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace AZEM.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class LoginPageModel : FreshMvvm.FreshBasePageModel
    {
        public LoginModel User { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand SignUpButtonCommand { get; set; }
        private bool _emailNotValid;
        private bool _passwordNotValid;
        public bool EmailNotValid { get => _emailNotValid;
            set
            {
                _emailNotValid = value; LoginCommand.RaiseCanExecuteChanged();
            }
        }
        public bool PasswordNotValid { get => _passwordNotValid;
            set
            {
                _passwordNotValid = value; LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            User = new LoginModel();
            LoginCommand = new RelayCommand(async () => await LoginAttempt(),ValidFields);
            SignUpButtonCommand = new RelayCommand(async () => await SignUpCommand());
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            EmailNotValid = true;
            PasswordNotValid = true;
            base.ViewIsAppearing(sender, e);
        }

        async Task LoginAttempt()
        {
            await Task.Delay(200);
            if (User.UserName == "defconm15@gmail.com" && User.Password == "freeee44")
                await CoreMethods.DisplayAlert("Test", "Test", "ok");
        }

        private bool ValidFields()
        {
            return !EmailNotValid && !PasswordNotValid;
        }


        async Task SignUpCommand()
        {
            await CoreMethods.PushPageModel<SignUpPageModel>();
        }
    }
}
