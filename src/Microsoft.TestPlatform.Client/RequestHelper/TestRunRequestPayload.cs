// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.VisualStudio.TestPlatform.Client.RequestHelper
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;

    /// <summary>
    /// Class used to define the TestRunRequestPayload sent by the Vstest.console translation layers into design mode
    /// </summary>
    public class TestRunRequestPayload
    {
        /// <summary>
        /// Gets or sets the sources for the test run request.
        /// </summary>
        /// <remarks> 
        /// Making this a list instead of an IEnumerable because the json serializer fails to deserialize
        /// if a linq query outputs the IEnumerable.
        /// </remarks>
        [DataMember]
        public List<string> Sources { get; set; }

        /// <summary>
        /// Gets or sets the test cases for the test run request.
        /// </summary>
        /// <remarks> 
        /// Making this a list instead of an IEnumerable because the json serializer fails to deserialize
        /// if a linq query outputs the IEnumerable.
        /// </remarks>
        [DataMember]
        public List<TestCase> TestCases { get; set; }

        /// <summary>
        /// Gets or sets the settings used for the test run request. 
        /// </summary>
        [DataMember]
        public string RunSettings { get; set; }

        /// <summary>
        /// Settings used for the Run request. 
        /// </summary>
        [DataMember]
        public bool KeepAlive { get; set; }

        /// <summary>
        /// Is Debugging enabled 
        /// </summary>
        [DataMember]
        public bool DebuggingEnabled { get; set; }
    }
}
