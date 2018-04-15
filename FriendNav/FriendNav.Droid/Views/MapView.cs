using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using FriendNav.Core.ViewModels;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using FriendNav.Droid.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
//using FriendNav.Core;

namespace FriendNav.Droid.Views
{
    
    [Activity(Label = "Map")]
    //public class MapView : BaseView, IOnMapReadyCallback,ILocationListener,IServiceConnection
    public class MapView : BaseView, IOnMapReadyCallback
    {
        
        protected override int LayoutResource => Resource.Layout.MapView;

        public string lattitude = "500";
        public string longitude = "500";

        //protected LocationManager _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);

        private GoogleMap GMap;
        private string test;

        GPSServiceBinder _binder;
        GPSServiceConnection _gpsServiceConnection;
        Intent _gpsServiceIntent;
        private GPSServiceReciever _receiver;

        MarkerOptions _options;

        // events, interfaced created in core library 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetUpMap();
            test = "in oncreate";

            _options = new MarkerOptions().SetTitle("CurrentPosition");

            _gpsServiceConnection = new GPSServiceConnection(_binder);
            _gpsServiceIntent = new Intent(Android.App.Application.Context, typeof(GPSService));
            BindService(_gpsServiceIntent, _gpsServiceConnection, Bind.AutoCreate);


        }

        private void RegisterBroadcastReceiver()
        {
            IntentFilter filter = new IntentFilter(GPSServiceReciever.LOCATION_UPDATED);
            filter.AddCategory(Intent.CategoryDefault);
            _receiver = new GPSServiceReciever(this);
            RegisterReceiver(_receiver, filter);
        }

        private void UnRegisterBroadcastReceiver()
        {
            UnregisterReceiver(_receiver);
        }

        private void SetUpMap()
        {
            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMap = googleMap;

            Permission permissionCheck = ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation);

            if (permissionCheck == Permission.Granted)
            {
                //Execute location service call if user has explicitly granted ACCESS_FINE_LOCATION..
                this.GMap.MyLocationEnabled = true;
            }
            
         
        }

        public void UpdateUI(Intent intent)
        {

            this.lattitude = intent.GetStringExtra("Lattitude");
            this.longitude = intent.GetStringExtra("Longitude");

            LatLng latlng = new LatLng(Convert.ToDouble(this.lattitude), Convert.ToDouble(this.longitude));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
            GMap.MoveCamera(camera);

            this._options.Dispose();
            this._options = new MarkerOptions().SetPosition(latlng).SetTitle("CurrentPosition");

            GMap.AddMarker(this._options);
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterBroadcastReceiver();
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnRegisterBroadcastReceiver();
        }
        [BroadcastReceiver]
        internal class GPSServiceReciever : BroadcastReceiver
        {
            public static readonly string LOCATION_UPDATED = "LOCATION_UPDATED";

            private MapView mapViewInstance;

            public GPSServiceReciever()
            {
                
            }

            public GPSServiceReciever(MapView instance)
            {
                this.mapViewInstance = instance;
            }
            
            public override void OnReceive(Context context, Intent intent)
            {
                this.mapViewInstance.UpdateUI(intent);
                

            }
        }


    }
}