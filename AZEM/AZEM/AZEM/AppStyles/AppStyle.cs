using AZEM.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AZEM.AppStyles
{
    class AppStyle : Application
    {
        #region Colors
        private static readonly Color lightBlue = Color.FromHex("#2EB2FF");
        private static readonly Color darkBlue = Color.FromHex("#4259CB");
        #endregion
        private static readonly String font = "Roboto.ttf";
        #region styles
        //public static readonly Style LightBlueButton(typeof(Button))
        //{
        //    new Setter { Property = Button.BackgroundColorProperty, Value = lightBlue };
        //    new Setter { Property = Button.BorderRadiusProperty, Value = 2 };
        //    new Setter { Property = Button.TextColorProperty, Value = Color.White };
        //}

        #endregion
    }
}
