// Copyright (c) Microsoft. All rights reserved.

namespace TestPlatform.CrossPlatEngine.UnitTests.Client
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;
    using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;
    using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Client;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using TestPlatform.Common.UnitTests.ExtensionFramework;

    [TestClass]
    public class ProxyDiscoveryManagerTests
    {
        private ProxyDiscoveryManager testDiscoveryManager;

        private Mock<ITestHostManager> mockTestHostManager;

        private Mock<ITestRequestSender> mockRequestSender;

        /// <summary>
        /// The client connection timeout in milliseconds for unit tests.
        /// </summary>
        private int testableClientConnectionTimeout = 400;

        [TestInitialize]
        public void TestInit()
        {
            this.mockTestHostManager = new Mock<ITestHostManager>();
            this.mockRequestSender = new Mock<ITestRequestSender>();
            this.testDiscoveryManager = new ProxyDiscoveryManager(this.mockRequestSender.Object, this.mockTestHostManager.Object, this.testableClientConnectionTimeout);
        }

        [TestMethod]
        public void InitializeShouldNotInitializeExtensionsOnNoExtensions()
        {
            // Make sure TestPlugincache is refreshed.
            TestPluginCache.Instance = null;

            this.testDiscoveryManager.Initialize(this.mockTestHostManager.Object);

            this.mockRequestSender.Verify(s => s.InitializeDiscovery(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void InitializeShouldInitializeExtensionsIfPresent()
        {
            // Make sure TestPlugincache is refreshed.
            TestPluginCache.Instance = null;

            try
            {
                var extensions = new string[] { "e1.dll", "e2.dll" };

                // Setup Mocks.
                TestPluginCacheTests.SetupMockAdditionalPathExtensions(extensions);
                this.mockRequestSender.Setup(s => s.WaitForRequestHandlerConnection(It.IsAny<int>())).Returns(true);

                this.testDiscoveryManager.Initialize(this.mockTestHostManager.Object);

                // Also verify that we have waited for client connection.
                this.mockRequestSender.Verify(s => s.WaitForRequestHandlerConnection(It.IsAny<int>()), Times.Once);
                this.mockRequestSender.Verify(
                    s => s.InitializeDiscovery(extensions, true),
                    Times.Once);
            }
            finally
            {
                TestPluginCache.Instance = null;
            }
        }

        [TestMethod]
        public void DiscoverTestsShouldNotIntializeIfDoneSoAlready()
        {
            this.testDiscoveryManager.Initialize(this.mockTestHostManager.Object);

            // Setup mocks.
            this.mockRequestSender.Setup(s => s.WaitForRequestHandlerConnection(It.IsAny<int>())).Returns(true);

            // Act.
            this.testDiscoveryManager.DiscoverTests(null, null);

            this.mockRequestSender.Verify(s => s.InitializeCommunication(), Times.AtMostOnce);
            this.mockTestHostManager.Verify(thl => thl.LaunchTestHost(null, It.IsAny<IList<string>>()), Times.AtMostOnce);
        }

        [TestMethod]
        public void DiscoverTestsShouldIntializeIfNotInitializedAlready()
        {
            // Setup mocks.
            this.mockRequestSender.Setup(s => s.WaitForRequestHandlerConnection(It.IsAny<int>())).Returns(true);

            // Act.
            this.testDiscoveryManager.DiscoverTests(null, null);

            this.mockRequestSender.Verify(s => s.InitializeCommunication(), Times.Once);
            this.mockTestHostManager.Verify(thl => thl.LaunchTestHost(null, It.IsAny<IList<string>>()), Times.Once);
        }

        [TestMethod]
        public void DiscoverTestsShouldThrowExceptionIfClientConnectionTimeout()
        {
            // Setup mocks.
            this.mockRequestSender.Setup(s => s.WaitForRequestHandlerConnection(It.IsAny<int>())).Returns(false);

            // Act.
            Assert.ThrowsException<TestPlatformException>(
                () => this.testDiscoveryManager.DiscoverTests(null, null));
        }


        [TestMethod]
        public void DiscoverTestsShouldInitiateServerDiscoveryLoop()
        {
            // Setup mocks.
            this.mockRequestSender.Setup(s => s.WaitForRequestHandlerConnection(It.IsAny<int>())).Returns(true);

            // Act.
            this.testDiscoveryManager.DiscoverTests(null, null);

            // Assert.
            this.mockRequestSender.Verify(s => s.DiscoverTests(null, null), Times.Once);
        }

        [TestMethod]
        public void DiscoverTestsShouldEndSessionWithTheServer()
        {
            // Setup mocks.
            this.mockRequestSender.Setup(s => s.WaitForRequestHandlerConnection(It.IsAny<int>())).Returns(true);

            // Act.
            this.testDiscoveryManager.DiscoverTests(null, null);

            // Assert.
            this.mockRequestSender.Verify(s => s.EndSession(), Times.Once);
        }

        private void SignalEvent(ManualResetEvent manualResetEvent)
        {
            // Wait for the 100 ms.
            Task.Delay(200).Wait();

            manualResetEvent.Set();
        }
    }
}
