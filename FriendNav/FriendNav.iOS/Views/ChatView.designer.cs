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
    [Register ("ChatView")]
    partial class ChatView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ChatInput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ChatRequest { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ChatSend { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ChatInput != null) {
                ChatInput.Dispose ();
                ChatInput = null;
            }

            if (ChatRequest != null) {
                ChatRequest.Dispose ();
                ChatRequest = null;
            }

            if (ChatSend != null) {
                ChatSend.Dispose ();
                ChatSend = null;
            }
        }
    }
}