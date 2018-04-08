using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using FriendNav.Core.Services.Interfaces;

namespace FriendNav.Droid.Services
{
    public class LocationUpdateService: ILocationUpdateService
    {
        //Android.Gms.Location.FusedLocationProviderClient fusedLocationProviderClient;
        string initiatorLatiude;

        public string getInitiatorLatitue() {

            return this.initiatorLatiude;
        }
    }
}