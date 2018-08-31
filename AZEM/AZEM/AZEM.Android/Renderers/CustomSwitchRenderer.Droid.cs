using System;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Plugin.CurrentActivity;
using AZEM.Controls;
using AZEM.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using FormColor = Xamarin.Forms.Color;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SwitchControl), typeof(CustomSwitchRenderer))]
namespace AZEM.Droid.Renderers
{
    public class CustomSwitchRenderer : SwitchRenderer
    {
        private FormColor falseColor;
        private FormColor trueColor;
        private SwitchControl ctrl;

        public CustomSwitchRenderer(Context ctx) : base(ctx)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                ctrl = (SwitchControl)e.NewElement;
                trueColor = ctrl.TrueColor;
                falseColor = ctrl.FalseColor;

                if (this.Control.Checked)
                {
                    this.Control.TrackDrawable.SetColorFilter(trueColor.ToAndroid(), PorterDuff.Mode.Multiply);
                    this.Control.ThumbDrawable.SetColorFilter(trueColor.ToAndroid(), PorterDuff.Mode.Multiply);
                }
                else
                {
                    this.Control.TrackDrawable.SetColorFilter(falseColor.ToAndroid(), PorterDuff.Mode.Multiply);
                    this.Control.ThumbDrawable.SetColorFilter(falseColor.ToAndroid(), PorterDuff.Mode.Multiply);
                }

                this.Control.CheckedChange += this.OnCheckedChange;
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            if (this.Control.Checked)
            {
                this.Element.IsToggled = true;
                this.Control.TrackDrawable.SetColorFilter(trueColor.ToAndroid(), PorterDuff.Mode.Multiply);
                this.Control.ThumbDrawable.SetColorFilter(trueColor.ToAndroid(), PorterDuff.Mode.Multiply);
            }
            else
            {
                this.Element.IsToggled = false;
                this.Control.TrackDrawable.SetColorFilter(falseColor.ToAndroid(), PorterDuff.Mode.Multiply);
                this.Control.ThumbDrawable.SetColorFilter(falseColor.ToAndroid(), PorterDuff.Mode.Multiply);
            }
        }
    }
}