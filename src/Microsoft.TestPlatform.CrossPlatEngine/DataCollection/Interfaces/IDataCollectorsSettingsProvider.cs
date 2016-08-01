// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.DataCollection.Interfaces
{
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

    /// <summary>
    /// The DataCollectorsSettingsProvider interface.
    /// </summary>
    public interface IDataCollectorsSettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Gets run specific data collection settings.
        /// </summary>
        DataCollectionRunSettings Settings { get; }
    }
}