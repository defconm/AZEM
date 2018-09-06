using AZEM.PageModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AZEM
{
	public partial class App : Application
	{
		public App ()
		{
            InitializeComponent();
            var LoginPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var navContainer = new FreshMvvm.FreshNavigationContainer(LoginPage);
            MainPage = navContainer;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
