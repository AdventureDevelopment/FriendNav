using Foundation;
using FriendNav.iOS.UserInterface;
using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace FriendNav.iOS.TableViewSource
{
    public class ConversationTableViewSource : MvxTableViewSource
    {
        public ConversationTableViewSource(UITableView tableView) : base(tableView)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (ConversationCell)tableView.DequeueReusableCell("chat_conversation", indexPath);
            return cell;
        }
    }
}
