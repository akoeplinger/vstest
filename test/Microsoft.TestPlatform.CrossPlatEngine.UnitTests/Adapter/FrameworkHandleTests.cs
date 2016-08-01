// Copyright (c) Microsoft. All rights reserved.

namespace TestPlatform.CrossPlatEngine.UnitTests.Adapter
{
    using System;

    using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Adapter;
    using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Execution;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
    using Moq;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;

    [TestClass]
    public class FrameworkHandleTests
    {
        [TestMethod]
        public void EnableShutdownAfterTestRunShoudBeFalseByDefault()
        {
            var tec = new TestExecutionContext(
                100,
                TimeSpan.MaxValue,
                inIsolation: false,
                keepAlive: false,
                areTestCaseLevelEventsRequired: false,
                isDebug: false,
                testCaseFilter: string.Empty);
            var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);

            Assert.IsFalse(frameworkHandle.EnableShutdownAfterTestRun);
        }

        [TestMethod]
        public void EnableShutdownAfterTestRunShoudBeSetAppropriately()
        {
            var tec = new TestExecutionContext(
                100,
                TimeSpan.MaxValue,
                inIsolation: false,
                keepAlive: false,
                areTestCaseLevelEventsRequired: false,
                isDebug: false,
                testCaseFilter: string.Empty);
            var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);

            frameworkHandle.EnableShutdownAfterTestRun = true;

            Assert.IsTrue(frameworkHandle.EnableShutdownAfterTestRun);
        }

        [TestMethod]
        public void LaunchProcessWithDebuggerAttachedShouldThrowIfObjectIsDisposed()
        {
            var tec = new TestExecutionContext(
                100,
                TimeSpan.MaxValue,
                inIsolation: false,
                keepAlive: false,
                areTestCaseLevelEventsRequired: false,
                isDebug: false,
                testCaseFilter: string.Empty);
            var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);
            frameworkHandle.Dispose();

            Assert.ThrowsException<ObjectDisposedException>(() => frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null));
        }

        // TODO: Enable method once we fix the "IsDebug" in TestExecutionContext
        // [TestMethod]
        public void LaunchProcessWithDebuggerAttachedShouldThrowIfNotInDebugContext()
        {
            var tec = new TestExecutionContext(
                100,
                TimeSpan.MaxValue,
                inIsolation: false,
                keepAlive: false,
                areTestCaseLevelEventsRequired: false,
                isDebug: false,
                testCaseFilter: string.Empty);
            var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }), tec, null);

            var isExceptionThrown = false;
            try
            {
                frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null);
            }
            catch (InvalidOperationException exception)
            {
                isExceptionThrown = true;
                Assert.AreEqual("This operation is not allowed in the context of a non-debug run.", exception.Message);
            }

            Assert.IsTrue(isExceptionThrown);
        }

        [TestMethod]
        public void LaunchProcessWithDebuggerAttachedShouldCallRunEventsHandler()
        {
            var tec = new TestExecutionContext(
                100,
                TimeSpan.MaxValue,
                inIsolation: false,
                keepAlive: false,
                areTestCaseLevelEventsRequired: false,
                isDebug: true,
                testCaseFilter: string.Empty);

            var mockTestRunEventsHandler = new Mock<ITestRunEventsHandler>();

            var frameworkHandle = new FrameworkHandle(null, new TestRunCache(100, TimeSpan.MaxValue, (s, r, ip) => { }),
                tec, mockTestRunEventsHandler.Object);

            frameworkHandle.LaunchProcessWithDebuggerAttached(null, null, null, null);

            mockTestRunEventsHandler.Verify(mt =>
                mt.LaunchProcessWithDebuggerAttached(It.IsAny<TestProcessStartInfo>()), Times.Once);
        }
    }
}
