
using System;
using System.Drawing;

using Foundation;
using FriendNav.Core.ViewModels;
using FriendNav.iOS.TableViewSource;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace FriendNav.iOS.Views
{
    [MvxFromStoryboard]
    public partial class ChatView : MvxViewController
    {
        public ChatView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<ChatView, ChatViewModel>();

            var chatTableViewSource = new ConversationTableViewSource(ConversationListTable);
            set.Bind(chatTableViewSource).To(vm => vm.Messages);
            set.Bind(MapButton).To(t => t.SendNavigationRequestCommand);
            set.Bind(SendButton).To(t => t.AddNewMessageCommand);
            set.Bind(MessageTextBox).To(t => t.ActiveMessage);
            set.Apply();

            ConversationListTable.Source = chatTableViewSource;
            ConversationListTable.ReloadData();
        }
    }
}