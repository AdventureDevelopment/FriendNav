using FriendNav.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FriendNav.Core.Services.Interfaces
{
    public interface ILocationUpdateService
    {
        EventHandler<LocationChangeEventArgs> LocationChanged { get; set; }
    }
}
