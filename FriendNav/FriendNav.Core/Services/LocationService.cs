using FriendNav.Core.Services.Interfaces;
using FriendNav.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FriendNav.Core.Services
{
    public class LocationService : ILocationUpdateService
    {
        public EventHandler<LocationChangeEventArgs> LocationChanged { get; }
    }
}
