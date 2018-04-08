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

namespace FriendNav.Core.ViewModels
{
    public class MapViewModel : MvxViewModel<Map>
    {
        private Map _map;
        private readonly ITask _task;
        private readonly IMapRepository _mapRepository;
        private readonly IUserRepository _userRepository;
        private readonly INavigateRequestRepository _navigateRequestRepository;
        private readonly INavigationRequestService _navigationRequestService;
        private readonly IMvxNavigationService _mvxNavigationService;
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly ILocationUpdateService _locationUpdateService;

        private bool _isCallingActivityInitiator;

        public MvxCommand SendNavigationFriendListRequestCommand { get; }
        public MvxCommand OnLocationChangeCommand { get; }

        public IAsyncHook TestNavigationHook { get; set; }
        public IAsyncHook TestLocationChangeHook { get; set; }

        public MapViewModel(ITask task,
            IMapRepository mapRepository,
            IUserRepository userRepository,
            INavigateRequestRepository navigateRequestRepository,
            INavigationRequestService navigationRequestService,
            IMvxNavigationService mvxNavigationService,
            IFirebaseAuthService firebaseAuthService,
            ILocationUpdateService locationUpdateService
            )
        {
            _task = task;
            _mapRepository = mapRepository;
            _userRepository = userRepository;
            _navigateRequestRepository = navigateRequestRepository;
            _navigationRequestService = navigationRequestService;
            _mvxNavigationService = mvxNavigationService;
            _firebaseAuthService = firebaseAuthService;
            _locationUpdateService = locationUpdateService;


            _locationUpdateService.LocationChanged += LocationUpdateService_LocationChanged;

            SendNavigationFriendListRequestCommand = new MvxCommand(SendEndNavigationAndMarkAsEnded);
        }

        private void LocationUpdateService_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            _map.UpdateActiveUserCords(e.Latitude, e.Longitude);
            _mapRepository.UpdateMap(_map);
            CurrentUserLatitude = e.Latitude;
            CurrentUserLongitude = e.Longitude;
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

        private void SendEndNavigationAndMarkAsEndedAsync()
        {
            _task.Run(SendEndNavigationAndMarkAsEnded);
        }

        private void SendEndNavigationAndMarkAsEnded()
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


            NavigateToChat();
        }

        private void NavigateToChat()
        {
            var user = _userRepository.GetUser(_firebaseAuthService.FirebaseAuth.User.Email);
            _mvxNavigationService.Navigate<FriendListViewModel, User>(user).Wait();

            TestNavigationHook?.NotifyOtherThreads();
        }
    }
}
