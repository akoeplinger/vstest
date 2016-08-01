// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel.Client
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Basic metadata for ITestPlaform.
    /// </summary>
    /// <remarks>
    /// This interface is only public due to limitations in MEF which require metadata interfaces
    /// to be public.  This interface is not intended for external consumption.
    /// </remarks>
    [SuppressMessage("Microsoft.MSInternal", "CA903:InternalNamespaceShouldNotContainPublicTypes", Justification = "This interface is only public due to limitations in MEF which require metadata interfaces to be public.")]
    public interface ITestPlatformCapabilities
    {
        /// <summary>
        /// Type of testPlatform
        /// </summary>
        TestPlatformType TestPlatformType { get; }
    }

    public enum TestPlatformType
    {
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        InProc,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        OutOfProc
    }
}
