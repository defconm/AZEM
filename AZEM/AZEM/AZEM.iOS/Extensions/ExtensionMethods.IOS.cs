using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using AZEM.Controls;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace AZEM.iOS.Extensions
{
    public static partial class ExtensionMethods 
    {
        /// <summary>
        /// Tos the local notification.
        /// </summary>
        /// <returns>The local notification.</returns>
        /// <param name="userInfo">User info.</param>
        //public static LocalNotification ToLocalNotification(this NSDictionary userInfo)
        //{
        //    var notification = new LocalNotification();
        //    if (null != userInfo && userInfo.ContainsKey(new NSString("aps")))
        //    {
        //        NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
        //        NSDictionary alert = null;
        //        if (aps.ContainsKey(new NSString("alert")))
        //            alert = aps.ObjectForKey(new NSString("alert")) as NSDictionary;
        //        if (alert != null)
        //        {
        //            notification.Title = (alert[new NSString("title")] as NSString).ToString();
        //            notification.SubTitle = (alert[new NSString("subtitle")] as NSString).ToString();
        //            notification.Message = (alert[new NSString("body")] as NSString).ToString();
        //            if (aps.ContainsKey(new NSString("badge")))
        //            {
        //                var cnt = (alert[new NSString("badge")] as NSString).ToString();
        //                notification.Badge = int.Parse(cnt);
        //            }
        //        }
        //    }
        //    return notification;
        //}
        /// <summary>
        /// Changes the color of the image.
        /// </summary>
        /// <returns>The image color.</returns>
        /// <param name="image">Image.</param>
        /// <param name="color">Color.</param>
        public static UIImage ChangeImageColor(this UIImage image, UIColor color)
        {
            var rect = new CGRect(0, 0, image.Size.Width, image.Size.Height);
            UIGraphics.BeginImageContext(rect.Size);
            var ctx = UIGraphics.GetCurrentContext();
            ctx.ClipToMask(rect, image.CGImage);
            ctx.SetFillColor(color.CGColor);
            ctx.FillRect(rect);
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return UIImage.FromImage(img.CGImage, 1.0f, UIImageOrientation.DownMirrored);
        }

        public static UIImage MaskeWithColor(this UIImage image, UIColor color)
        {

            var maskImage = image.CGImage;
            var width = image.Size.Width;
            var height = image.Size.Height;
            var bounds = new CGRect(0, 0, width, height);

            using (var colorSpace = CGColorSpace.CreateDeviceRGB())
            {
                using (var bitmapContext = new CGBitmapContext(null, (nint)width, (nint)height, 8, 0, colorSpace, CGImageAlphaInfo.PremultipliedLast))
                {
                    bitmapContext.ClipToMask(bounds, maskImage);
                    bitmapContext.SetFillColor(color.CGColor);
                    bitmapContext.FillRect(bounds);

                    using (var cImage = bitmapContext.ToImage())
                    {
                        var coloredImage = UIImage.FromImage(cImage);
                        return coloredImage;
                    }
                }
            }

        }

        /// <summary>
        /// Resize the specified imgView and size.
        /// </summary>
        /// <returns>The resize.</returns>
        /// <param name="imgView">Image view.</param>
        /// <param name="size">Size.</param>
        public static void Resize(this UIImageView imgView, nfloat size)
        {
            var newSize = new CGSize(size, size);
            UIGraphics.BeginImageContextWithOptions(newSize, false, UIScreen.MainScreen.Scale);
            imgView.Image.Draw(new CGRect(0, 0, newSize.Width, newSize.Height));
            imgView.Image = UIGraphics.GetImageFromCurrentImageContext();
            imgView.ContentMode = UIViewContentMode.ScaleAspectFit;
        }
        /// <summary>
        /// Gets the value from description.
        /// </summary>
        /// <returns>The value from description.</returns>
        /// <param name="value">Value.</param>
        public static UIReturnKeyType GetValueFromDescription(this ReturnKeyTypes value)
        {
            var type = typeof(UIReturnKeyType);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == value.ToString())
                        return (UIReturnKeyType)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString())
                        return (UIReturnKeyType)field.GetValue(null);
                }
            }
            throw new NotSupportedException($"Not supported on iOS: {value}");
        }

        public static nfloat StringHeight(this string text, UIFont font, nfloat width)
        {
            var nativeString = new NSString(text);

            var rect = nativeString.GetBoundingRect(
                new CGSize(width, nfloat.MaxValue),
                NSStringDrawingOptions.UsesLineFragmentOrigin,
                new UIStringAttributes() { Font = font },
                null);

            return rect.Height;
        }

        public static ImageSource GetImageResource<T>(this T obj, string imgName) where T : class
        {
            var assemblyName = Assembly.GetAssembly(obj.GetType()).FullName;
            return ImageSource.FromResource($"{assemblyName}.{imgName}");
        }


        #region Data

        /// <summary>The NSDate from Xamarin takes a reference point form January 1, 2001, at 12:00</summary>
        /// <remarks>
        /// It also has calls for NIX reference point 1970 but appears to be problematic
        /// </remarks>
        private static DateTime nsRef = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0, 0)); // last zero is milliseconds

        #endregion

        /// <summary>Returns the seconds interval for a DateTime from NSDate reference data of January 1, 2001</summary>
        /// <param name="dt">The DateTime to evaluate</param>
        /// <returns>The seconds since NSDate reference date</returns>
        public static double SecondsSinceNSRefenceDate(this DateTime dt)
        {
            return (dt - nsRef).TotalSeconds;
        }


        /// <summary>Convert a DateTime to NSDate</summary>
        /// <param name="dt">The DateTime to convert</param>
        /// <returns>An NSDate</returns>
        public static NSDate ToNSDate(this DateTime dt)
        {
            return NSDate.FromTimeIntervalSinceReferenceDate(dt.SecondsSinceNSRefenceDate());
        }


        /// <summary>Convert an NSDate to DateTime</summary>
        /// <param name="nsDate">The NSDate to convert</param>
        /// <returns>A DateTime</returns>
        public static DateTime ToDateTime(this NSDate nsDate)
        {
            // We loose granularity below millisecond range but that is probably ok
            return nsRef.AddSeconds(nsDate.SecondsSinceReferenceDate);
        }
    }
}