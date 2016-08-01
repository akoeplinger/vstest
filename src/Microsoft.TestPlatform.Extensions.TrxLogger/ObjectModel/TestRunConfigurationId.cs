// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.TestPlatform.Extensions.TrxLogger.ObjectModel
{
    using System;

    /// <summary>
    /// The test run configuration id.
    /// </summary>
    internal sealed class TestRunConfigurationId
    {
        private Guid id;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunConfigurationId"/> class.
        /// </summary>
        public TestRunConfigurationId()
        {
            this.id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id
        {
            get { return this.id; }
        }
    }
}
