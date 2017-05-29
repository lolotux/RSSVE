//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016-2017, Alexander "Phineas Freak" Kampolis.

//  This file is part of Real Solar System Visual Enhancements.

//  Real Solar System Visual Enhancements is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0
//  (CC-BY-NC-SA 4.0) license.

//  You should have received a copy of the license along with this work. If not, visit the official
//  Creative Commons web page:

//      • https://www.creativecommons.org/licensies/by-nc-sa/4.0

//  Based on the InstallChecker from the Kethane mod for Kerbal Space Program:
//
//      • https://github.com/Majiir/Kethane/blob/master/Plugin/Kethane/Utilities/InstallChecker.cs
//
//  Original is © Copyright Majiir, CC0 Public Domain:
//
//      • http://creativecommons.org/publicdomain/zero/1.0
//
//  Forum thread:
//
//      • http://forum.kerbalspaceprogram.com/index.php?showtopic=59388
//
//  This file has been modified extensively and is released under the same license.
//  ================================================================================

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RSSVE
{
    /// <summary>
    /// Version and file system integrity checker class. Operates only in the Main Menu scene.
    /// </summary>

    [KSPAddon(KSPAddon.Startup.MainMenu, true)]

    class InstallChecker : MonoBehaviour
    {
        /// <summary>
        /// Method to start the installation checker.
        /// </summary>

        protected void Start ()
        {
            try
            {
                //  Search for this mod's DLL existing in the wrong location. This will also detect duplicate copies because only one can be in the right place.

                var assemblies = AssemblyLoader.loadedAssemblies.Where (asm => asm.assembly.GetName ().Name.Equals (Assembly.GetExecutingAssembly ().GetName ().Name)).Where (asm => asm.url != Constants.AssemblyPath);

                if (assemblies.Any ())
                {
                    var BadPaths = assemblies.Select (asm => asm.path).Select (p => Uri.UnescapeDataString (new Uri(Path.GetFullPath (KSPUtil.ApplicationRootPath)).MakeRelativeUri (new Uri (p)).ToString ().Replace ('/', Path.DirectorySeparatorChar)));

                    var BadPathsString = string.Join ("\n", BadPaths.ToArray ());

                    Notification.Logger (Constants.AssemblyName, "Error", string.Format ("Incorrect installation, bad path(s): {0}", BadPathsString));

                    Notification.Dialog ("BaseAssemblyChecker", string.Format ("Incorrect {0} Installation", Constants.AssemblyName), "#F0F0F0", string.Format ("{0} has been installed incorrectly and will not function properly. All files should be located under the GameData" + Path.AltDirectorySeparatorChar + Constants.AssemblyName + "folder. Do not move any files from inside that folder!\n\nIncorrect path(s):\n    •    {1}", Constants.AssemblyName, BadPathsString), "#F0F0F0");
                }

                string MissingDependenciesNames = string.Empty;

                int MissingDependenciesCount = 0;

                //  Check if Environmental Visual Enhancements is installed.

                if (!AssemblyLoader.loadedAssemblies.Any (asm => asm.assembly.GetName ().Name.StartsWith ("EVEManager", StringComparison.InvariantCultureIgnoreCase) && asm.url.ToLower ().Equals ("environmentalvisualenhancements" + Path.AltDirectorySeparatorChar + "plugins")))
                {
                    MissingDependenciesNames += "  •  Environmental Visual Enhancements\n";
                    MissingDependenciesCount += 1;

                    Notification.Logger (Constants.AssemblyName, "Warning", "Missing or incorrectly installed Environmental Visual Enhancements!");
                }

                //  Check if Scatterer is installed.

                if (!AssemblyLoader.loadedAssemblies.Any (asm => asm.assembly.GetName ().Name.StartsWith ("Scatterer", StringComparison.InvariantCultureIgnoreCase) && asm.url.ToLower ().Equals ("scatterer")))
                {
                    MissingDependenciesNames += "  •  Scatterer\n";
                    MissingDependenciesCount += 1;

                    Notification.Logger (Constants.AssemblyName, "Warning", "Missing or incorrectly installed Scatterer!");
                }

                //  Check it Module Manager is installed.

                if (!AssemblyLoader.loadedAssemblies.Any (asm => asm.assembly.GetName ().Name.StartsWith ("ModuleManager", StringComparison.InvariantCultureIgnoreCase) && asm.url.ToLower ().Equals (string.Empty)))
                {
                    MissingDependenciesNames += "  •  Module Manager\n";
                    MissingDependenciesCount += 1;

                    Notification.Logger (Constants.AssemblyName, "Warning", "Missing or incorrectly installed Module Manager!");
                }

                //  Warn the user if any of the dependencies are missing.

                if (!MissingDependenciesCount.Equals (0))
                {
                    Notification.Dialog ("DependencyChecker", "Missing Dependencies", "#F0F0F0", string.Format ("{0} requires the following listed mods in order to function correctly:\n\n  {1}", Constants.AssemblyName, MissingDependenciesNames.Trim ()), "#F0F0F0");

                    Notification.Logger (Constants.AssemblyName, "Error", "Required dependencies missing!");
                }
            }
            catch (Exception ex)
            {
                Notification.Logger (Constants.AssemblyName, "Error", string.Format ("{0}: Caught an exception:\n{1}\n", ex.Message, ex.StackTrace));

                Notification.Dialog ("ExceptionChecker", string.Format ("Incorrect {0} installation", Constants.AssemblyName), "#F0F0F0",
                                     string.Format ("An error has occurred while checking the installation of {0}.\n\n", Constants.AssemblyName) +
                                     string.Format ("You need to:\n" +
                                     "  •  Terminate the KSP instance\n" +
                                     "  •  Send a complete copy of the 'output.log' file to the mod developer\n" +
                                     "  •  Completely remove and re-install {0} and it's required mods\n", Constants.AssemblyName), "#F0F0F0");
            }
        }
    }
}
