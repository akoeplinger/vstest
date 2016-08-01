// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;

    /// <summary>
    /// The discovery complete payload.
    /// </summary>
    public class DiscoveryCompletePayload
    {
        /// <summary>
        /// Gets or sets the total number of tests discovered.
        /// </summary>
        public long TotalTests { get; set; }

        /// <summary>
        /// Gets or sets the last chunk of discovered tests.
        /// </summary>
        public IEnumerable<TestCase> LastDiscoveredTests { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether discovery was aborted.
        /// </summary>
        public bool IsAborted { get; set; }
    }
}
