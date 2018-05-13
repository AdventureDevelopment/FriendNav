using System;
using Foundation;
using FriendNav.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace FriendNav.iOS.UserInterface
{
    public partial class ConversationCell : MvxTableViewCell
    {
        public ConversationCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ConversationCell, MessageViewModel>();
                set.Bind(MessageLabel).To(m => m.Text);
                set.Apply();
            });
        }
    }
}
