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
using Android.Locations;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using FriendNav.Core.ViewModels;

namespace FriendNav.Droid.Services
{
    class GPSService : Service,ILocationListener
    {

        IBinder _binder;
        private string test;
        //public Context CurrentContext => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
       // private readonly NotificationService _notificationService;
        protected LocationManager _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);

        

        //public object Vm { get => vm; set => vm = value; }

        public override IBinder OnBind(Intent intent)
        {
            //throw new NotImplementedException();
            _binder = new GPSServiceBinder(this);
            return _binder;
        }

        public void StartLocationUpdates()
        {
            Criteria criteriaForGPSService = new Criteria
            {
                //A constant indicating an approximate accuracy  
                Accuracy = Accuracy.Coarse,
                PowerRequirement = Power.Medium
            };

            var locationProvider = _locationManager.GetBestProvider(criteriaForGPSService, true);
            _locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);

            

        }

        public void OnLocationChanged(Location location)
        {
            if (null != location)
            {
                double latitude = location.Latitude;
                double longitude = location.Longitude;
                //LatLng latlng = new LatLng(latitude, longitude);

                Intent intent = new Intent(this, typeof(MapView));


            }
            else
            {
                test = "came here with null location";
            }
            // throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
           // throw new NotImplementedException();
        }
    }

     class GPSServiceBinder : Binder
    {
        public GPSService Service { get { return this.LocService; } }
        protected GPSService LocService;
        public bool IsBound { get; set; }
        public GPSServiceBinder(GPSService service) { this.LocService = service; }
    }

     class GPSServiceConnection : Java.Lang.Object, IServiceConnection
    {

        GPSServiceBinder _binder;

        public event Action Connected;
        public GPSServiceConnection(GPSServiceBinder binder)
        {
            if (binder != null)
                this._binder = binder;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            GPSServiceBinder serviceBinder = (GPSServiceBinder)service;

            if (serviceBinder != null)
            {
                this._binder = serviceBinder;
                this._binder.IsBound = true;
                serviceBinder.Service.StartLocationUpdates();
                if (Connected != null)
                    Connected.Invoke();
            }
        }
        public void OnServiceDisconnected(ComponentName name) { this._binder.IsBound = false; }
    }
}

//service interface/ one event/