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

        protected LocationManager _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);

        private GoogleMap GMap;
        private string test;

        GPSServiceBinder _binder;
        GPSServiceConnection _gpsServiceConnection;
        Intent _gpsServiceIntent;

        // events, interfaced created in core library 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetUpMap();
            test = "in oncreate";

            _gpsServiceConnection = new GPSServiceConnection(_binder);
            _gpsServiceIntent = new Intent(Android.App.Application.Context, typeof(GPSService));
            BindService(_gpsServiceIntent, _gpsServiceConnection, Bind.AutoCreate);


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
            
            //var t = (MapViewModel)ViewModel;

            LatLng latlng = new LatLng(Convert.ToDouble(13.0291), Convert.ToDouble(80.2083));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
            GMap.MoveCamera(camera);
            MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle("Chennai");
            GMap.AddMarker(options);

         
        }


    }
}