// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Microsoft.TestPlatfrom.ObjectModel")]
[assembly: AssemblyTrademark("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8a200cda-4813-43a1-aa18-9faedc31d2af")]


// Type forwarding utility classes defined earlier in object model to a core utilities assembly.
[assembly: TypeForwardedTo(typeof(EqtTrace))]
[assembly: TypeForwardedTo(typeof(ValidateArg))]