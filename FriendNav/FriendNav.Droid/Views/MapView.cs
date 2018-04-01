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
//using FriendNav.Core;

namespace FriendNav.Droid.Views
{
    [Activity(Label = "Map")]
    public class MapView : BaseView, IOnMapReadyCallback,ILocationListener
    {
        protected override int LayoutResource => Resource.Layout.MapView;

        private GoogleMap GMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetUpMap();
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

           /* LatLng latlng = new LatLng(Convert.ToDouble(13.0291), Convert.ToDouble(80.2083));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
            GMap.MoveCamera(camera);
            MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle("Chennai");
            GMap.AddMarker(options);*/


        }

        public void OnLocationChanged(Location location)
        {
            //throw new NotImplementedException();
            if (null != location)
            {
                double latitude = location.Latitude;
                double longitude = location.Longitude;
                LatLng latlng = new LatLng(latitude,longitude);
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
                GMap.MoveCamera(camera);
                MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle("MyLocation");
                GMap.AddMarker(options);
            }

        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}