using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AZEM
{
    class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
		  : base(handle, transer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();
			CrossCurrentActivity.Current.Init(this);
		}
    }
}
