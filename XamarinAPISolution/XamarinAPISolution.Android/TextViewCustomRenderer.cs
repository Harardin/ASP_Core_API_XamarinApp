﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinAPISolution.Droid;

[assembly: ExportRenderer(typeof(Entry), typeof(TextViewCustomRenderer))]
namespace XamarinAPISolution.Droid
{
    public class TextViewCustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Android.App.Application.Context.GetDrawable(Resource.Drawable.RoundCornerbutton);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(10, 0, 0, 0);
            }
        }
    }
}