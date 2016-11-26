//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016, Phineas Freak

//  This file is part of Real Solar System Visual Enhancements.

//  Real Solar System Visual Enhancements is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0
//  (CC-BY-NC-SA 4.0) license.

//  You should have received a copy of the license along with this work. If not, visit the official
//  Creative Commons web page:

//      • https://www.creativecommons.org/licensies/by-nc-sa/4.0
//  ================================================================================

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//  ================================================================================
//  General Information about the specified assembly.
//  ================================================================================

[assembly: AssemblyTitle("RSSVE")]
[assembly: AssemblyDescription("Real Solar System Visual Enhancements")]
[assembly: AssemblyProduct("RSSVE")]
[assembly: AssemblyCompany("Phineas Freak")]
[assembly: AssemblyCopyright("Copyright © 2016, Phineas Freak")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")] 

#if DEBUG
[assembly: AssemblyConfiguration("Develop")]
#else
[assembly: AssemblyConfiguration("Stable")]
#endif

//  ================================================================================
//  Hide the specified assembly from any COM components.
//  ================================================================================

[assembly: ComVisible(false)]

//  ================================================================================
//  Version information for the specified assembly.

//  Consists of the following four values:
//
//    • Major Version
//    • Minor Version 
//    • Build Number
//    • Revision
//  ================================================================================

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.2.1.1604")]
[assembly: AssemblyInformationalVersion("1.2.1.1604")]

[assembly: KSPAssembly("RSSVE", 1, 0)]
