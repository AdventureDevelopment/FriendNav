using FriendNav.Core.DataTransfer;
using FriendNav.Core.Model;
using FriendNav.Core.Repositories.Interfaces;
using FriendNav.Core.Services.Interfaces;
using FriendNav.Core.Utilities;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendNav.Core.ViewModels
{
    public class MapViewModel : MvxViewModel<Map>
    {
        private Map _map;
        private readonly IMapRepository _mapRepository;
        private readonly IUserRepository _userRepository;
        private readonly INavigateRequestRepository _navigateRequestRepository;
        private readonly INavigationRequestService _navigationRequestService;
        private readonly IMvxNavigationService _mvxNavigationService;
        private readonly IFirebaseAuthService _firebaseAuthService;

        private readonly ILocationUpdateService _locationUpdateService;

        private Boolean _endNavigation, _isCallingActivityInitiator;

        public MvxCommand SendNavigationFriendListRequestCommand { get; }
        public MvxCommand OnLocationChangeCommand { get; }

        public IAsyncHook TestNavigationHook { get; set; }
        public IAsyncHook TestLocationChangeHook { get; set; }

        public MapViewModel(
            IMapRepository mapRepository,
            IUserRepository userRepository,
            INavigateRequestRepository navigateRequestRepository,
            INavigationRequestService navigationRequestService,
            IMvxNavigationService mvxNavigationService,
            IFirebaseAuthService firebaseAuthService,
            ILocationUpdateService locationUpdateService
            )
        {
            _mapRepository = mapRepository;
            _userRepository = userRepository;
            _navigateRequestRepository = navigateRequestRepository;
            _navigationRequestService = navigationRequestService;
            _mvxNavigationService = mvxNavigationService;
            _firebaseAuthService = firebaseAuthService;

            SendNavigationFriendListRequestCommand = new MvxCommand(SendEndNavigationAndMarkAsEndedAsync);
            OnLocationChangeCommand = new MvxCommand(OnLocationChangedAsync);

            _locationUpdateService = locationUpdateService;
            _locationUpdateService.LocationChanged += LocationUpdateService_LocationChanged;


        }


        public async void LocationUpdateService_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            _map.UpdateActiveUserCords(e.Latitude, e.Longitude);
            
            CurrentUserLatitude = e.Latitude;
            CurrentUserLongitude = e.Longitude;

            await _mapRepository.UpdateMap(_map);
        }
        private void map_OtherUserCordinatesUpdated(object sender, EventArgs e)
        {
            if (_map.IsInitiator)
            {
                OtherUserLatitude = _map.InitiatorLatitude;
                OtherUserLongitude = _map.InitiatorLongitude;
            }
            else
            {
                OtherUserLatitude = _map.ResponderLatitude;
                OtherUserLongitude = _map.ResponderLongitude;
            }
        }


        public override void Prepare(Map parameter)
        {
            _map = parameter;
            _map.OtherUserCordinatesUpdated += map_OtherUserCordinatesUpdated;
        }
        private string _currentUserLatitude;
        public string CurrentUserLatitude
        {
            get => _currentUserLatitude;
            set
            {
                _currentUserLatitude = value;
                RaisePropertyChanged();
            }
        }


        private string _currentUserLongitude;
        public string CurrentUserLongitude
        {
            get => _currentUserLongitude;
            set
            {
                _currentUserLongitude = value;
                RaisePropertyChanged();
            }
        }

        private string _otherUserLatitude;
        public string OtherUserLatitude
        {
            get => _currentUserLatitude;
            set
            {
                _currentUserLatitude = value;
                RaisePropertyChanged();
            }
        }

        private string _otherUserLongitude;
        public string OtherUserLongitude
        {
            get => _otherUserLongitude;
            set
            {
                _otherUserLongitude = value;
                RaisePropertyChanged();
            }
        }

        private async void OnMapReady(Map map)
        {
            _map = await _mapRepository.GetMap(_map.ChatFirebaseKey);
        }

        private void OnLocationChangedAsync()
        {
            Task.Run(OnLocationChanged);
        }

        // map argument is from google location
        public async Task OnLocationChanged()
        {
            if (!_endNavigation)
            {
                _map = await _mapRepository.GetMap(_map.ChatFirebaseKey);

                TestLocationChangeHook?.NotifyOtherThreads();
            }
            return;
        }

        private void SendEndNavigationAndMarkAsEndedAsync()
        {
            Task.Run(SendEndNavigationAndMarkAsEnded);
        }

        public async Task SendEndNavigationAndMarkAsEnded()
        {
            if (_isCallingActivityInitiator)
            {
                _map.InitiatorLatitude = "500";
                _map.InitiatorLatitude = "500";
            }
            else
            {
                _map.ResponderLatitude = "500";
                _map.ResponderLongitude = "500";
            }


            await NavigateToChat();
        }

        private async Task NavigateToChat()
        {
            var user = await _userRepository.GetUser(_firebaseAuthService.FirebaseAuth.User.Email);
            await _mvxNavigationService.Navigate<FriendListViewModel, User>(user);

            TestNavigationHook?.NotifyOtherThreads();
        }
    }
}
