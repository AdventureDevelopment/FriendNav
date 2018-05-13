
using System;
using System.Drawing;

using Foundation;
using FriendNav.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace FriendNav.iOS.Views
{
    [MvxFromStoryboard]
    public partial class MapView : MvxViewController
    {
        public MapView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            var set = this.CreateBindingSet<MapView, MapViewModel>();
            set.Apply();
        }
    }
}