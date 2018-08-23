using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AZEM.Renderers;
using AZEM.Droid.Renderers;
using Android.Content;

[assembly: ExportRenderer(typeof(EntryUnderlineRender), typeof(EntryUnderline))]
namespace AZEM.Droid.Renderers
{
    class EntryUnderline : EntryRenderer
    {
        public EntryUnderline(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null)
                return;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }
    }
}