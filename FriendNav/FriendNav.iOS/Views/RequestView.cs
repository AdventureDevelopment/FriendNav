
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
    public partial class RequestView : MvxViewController
    {
        public RequestView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            var set = this.CreateBindingSet<RequestView, RequestViewModel>();
            set.Bind(RequestAcceptButton).To(vm => vm.AcceptRequestCommand);
            set.Bind(RequestRejectButton).To(vm => vm.DeclineRequestCommand);
            set.Apply();
        }
    }
}