// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FriendNav.iOS.Views
{
    [Register ("RequestView")]
    partial class RequestView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FriendNav.iOS.RequestAcceptButton RequestAcceptButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FriendNav.iOS.RequestRejectButton RequestRejectButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RequestAcceptButton != null) {
                RequestAcceptButton.Dispose ();
                RequestAcceptButton = null;
            }

            if (RequestRejectButton != null) {
                RequestRejectButton.Dispose ();
                RequestRejectButton = null;
            }
        }
    }
}