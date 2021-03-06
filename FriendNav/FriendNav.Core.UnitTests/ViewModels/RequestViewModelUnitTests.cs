﻿using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FriendNav.Core.ViewModels;
using FriendNav.Core.Repositories.Interfaces;
using FriendNav.Core.Services.Interfaces;
using Moq;
using MvvmCross.Core.Navigation;
using FriendNav.Core.Model;
using FriendNav.Core.ViewModelParameters;
using System.Threading.Tasks;

namespace FriendNav.Core.Tests.ViewModels
{
    [TestClass]
    public class RequestViewModelUnitTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }
        [TestMethod]
        public async Task User_Accepting_Nav_Request_Unit_Test()
        {
            var _navigationRequestService = new Mock<INavigationRequestService>();
            var _mapRepository = new Mock<IMapRepository>();

            var _mvxNavigationService = new Mock<IMvxNavigationService>();

            var chat = _fixture.Create<Chat>();
            var navigationRequest = _fixture.Create<NavigateRequest>();

            var sut = new RequestViewModel(
                _navigationRequestService.Object,
                _mapRepository.Object,
                _mvxNavigationService.Object
                );
            sut.Prepare(new NavigateRequestParameters { Chat = chat, NavigateRequest = navigationRequest });

            await sut.AcceptRequest();

            _mvxNavigationService.Verify(v => v.Navigate<MapViewModel, Map>(It.IsAny<Map>(), null));
        }

        [TestMethod]
        public async Task User_Decline_NavRequest_Unit_Test()
        {
            var _navigationRequestService = new Mock<INavigationRequestService>();
            var _mapRepository = new Mock<IMapRepository>();

            var _mvxNavigationService = new Mock<IMvxNavigationService>();

            var chat = _fixture.Create<Chat>();

            var navigationRequest = _fixture.Create<NavigateRequest>();
            var sut = new RequestViewModel(
                _navigationRequestService.Object,
                _mapRepository.Object,
                _mvxNavigationService.Object
                );
            sut.Prepare(new NavigateRequestParameters { Chat = chat, NavigateRequest = navigationRequest });

            await sut.DeclineRequest();

            _mvxNavigationService.Verify(v => v.Navigate<ChatViewModel, ChatParameters>(It.Is<ChatParameters>(i => i.Chat == chat), null));

        }
    }
}
