﻿using Autofac;
using FriendNav.Core.IntegrationTests.TestModel;
using FriendNav.Core.Model;
using FriendNav.Core.Repositories.Interfaces;
using FriendNav.Core.Services.Interfaces;
using FriendNav.Core.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendNav.Core.IntegrationTests.ViewModels
{
    [TestClass]
    public class MapViewModelTests
    {
        public MapViewModelTests()
        {
        }

        [TestMethod]
        public void Navigate_to_friend_list_view_model()
        {
            var context = TestAppContext.ConstructTestAppContext();

            var mapRepository = context.TestContainer.Resolve<IMapRepository>();
            var userRepository = context.TestContainer.Resolve<IUserRepository>();
            var navigateRequestRepository = context.TestContainer.Resolve<INavigateRequestRepository>();
            var navigationRequestService = context.TestContainer.Resolve<INavigationRequestService>();
            var mvxNavigationService = context.TestContainer.Resolve<IMvxNavigationService>();
            var firebaseAuthService = context.TestContainer.Resolve<IFirebaseAuthService>();

            var mapViewModel = context.TestContainer.Resolve<MapViewModel>();

            firebaseAuthService.LoginUser("c@test.com", "theday");

            mapViewModel.Prepare(new Model.Map());

            mapViewModel.SendNavigationFriendListRequestCommand.Execute();

            context.MockNavigationService.Verify(v => v.Navigate<FriendListViewModel, User>(It.IsAny<User>(), null));

            mapRepository.Dispose();
            userRepository.Dispose();
            navigateRequestRepository.Dispose();
        }

        [TestMethod]
        public void On_location_change()
        {
            var context = TestAppContext.ConstructTestAppContext();

            var mapRepository = context.TestContainer.Resolve<IMapRepository>();
            var userRepository = context.TestContainer.Resolve<IUserRepository>();
            var navigateRequestRepository = context.TestContainer.Resolve<INavigateRequestRepository>();
            var navigationRequestService = context.TestContainer.Resolve<INavigationRequestService>();
            var mvxNavigationService = context.TestContainer.Resolve<IMvxNavigationService>();
            var firebaseAuthService = context.TestContainer.Resolve<IFirebaseAuthService>();

            var mapViewModel = context.TestContainer.Resolve<MapViewModel>();

            firebaseAuthService.LoginUser("c@test.com", "theday");

            var map = mapRepository.GetMap("c@test,comc1@test,com");

            mapViewModel.Prepare(map);

            var testHook = new MapViewModelHook
            {
                Map = new Map
                {
                    InitiatorLatitude = "499",
                    InitiatorLongitude = "499"
                }
            };

            mapViewModel.TestLocationChangeHook = testHook;

            mapViewModel.OnLocationChangeCommand.Execute();

            // testHook.ResetEvent.WaitOne();

            Assert.AreEqual("498", testHook.Map.InitiatorLatitude);
            Assert.AreEqual("498", testHook.Map.InitiatorLongitude);

            mapRepository.Dispose();
            userRepository.Dispose();
            navigateRequestRepository.Dispose();
        }
    }
}