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
        UIKit.UITableView ConversationListTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton MapButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField MessageTextBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SendButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ConversationListTable != null) {
                ConversationListTable.Dispose ();
                ConversationListTable = null;
            }

            if (MapButton != null) {
                MapButton.Dispose ();
                MapButton = null;
            }

            if (MessageTextBox != null) {
                MessageTextBox.Dispose ();
                MessageTextBox = null;
            }

            if (SendButton != null) {
                SendButton.Dispose ();
                SendButton = null;
            }
        }
    }
}