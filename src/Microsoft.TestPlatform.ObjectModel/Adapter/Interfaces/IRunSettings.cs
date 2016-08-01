// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter
{
    /// <summary>
    /// Used for loading settings for a run.
    /// </summary>
    public interface IRunSettings
    {
        /// <summary>
        /// Get the settings for the provided settings name.
        /// </summary>
        /// <param name="settingsName">Name of the settings section to get.</param>
        /// <returns>The settings provider for the settings or null if one was not found.</returns>
        ISettingsProvider GetSettings(string settingsName);

        /// <summary>
        /// Settings used for this run.
        /// </summary>
        string SettingsXml { get; }
    }
}
