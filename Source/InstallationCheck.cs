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
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]

    class InstallChecker : MonoBehaviour
    {
        /// <summary>
        /// Method to start the installation checker. Operates only in the Main Menu scene.
        /// </summary>

        protected void Start()
        {
            try
            {
                //  Log some basic information that might be of interest when debugging.

                Notification.Logger(Constants.AssemblyName, "Assembly location: " + Assembly.GetExecutingAssembly().Location);
                Notification.Logger(Constants.AssemblyName, "Assembly version: " + Version.AssemblyVersion);
                Notification.Logger(Constants.AssemblyName, "Assembly compatibility: " + Constants.VersionCompatible.Major + "." + Constants.VersionCompatible.Minor  + "." + Constants.VersionCompatible.Revis);

               //  Search for this mod's DLL existing in the wrong location. This will also detect duplicate copies because only one can be in the right place.

                var assemblies = AssemblyLoader.loadedAssemblies.Where(a => a.assembly.GetName().Name == Assembly.GetExecutingAssembly().GetName().Name).Where(a => a.url != Constants.AssemblyPath);

                if (assemblies.Any())
                {
                    var badPaths = assemblies.Select(a => a.path).Select(p => Uri.UnescapeDataString(new Uri(Path.GetFullPath(KSPUtil.ApplicationRootPath)).MakeRelativeUri(new Uri(p)).ToString().Replace('/', Path.DirectorySeparatorChar)));

                    string badPathsString = string.Join("\n", badPaths.ToArray());

                    Notification.Logger(Constants.AssemblyName, "Incorrect installation, bad path(s):\n" + badPathsString);

                    Notification.Dialog("Incorrect " + Constants.AssemblyName + " Installation", Constants.AssemblyName + " has been installed incorrectly and will not function properly. All files should be located in KSP/GameData/" + Constants.AssemblyName + ". Do not move any files from inside that folder.\n\nIncorrect path(s):\n" + badPathsString);
                }

                //  Check if Environmental Visual Enhancements is installed.

                if (!AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName ().Name.StartsWith ("EVEManager", StringComparison.InvariantCultureIgnoreCase) && a.url.ToLower() == "environmentalvisualenhancements/plugins"))
                {
                    Notification.Logger(Constants.AssemblyName, "Missing or incorrectly installed Environmental Visual Enhancements.");

                    Notification.Dialog("Missing Environmental Visual Enhancements", Constants.AssemblyName + " requires the Environmental Visual Enhancements mod in order to function properly.\n");
                }

                //  Check if Scatterer is installed.

                if (!AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName ().Name.StartsWith ("Scatterer", StringComparison.InvariantCultureIgnoreCase) && a.url.ToLower() == "scatterer"))
                {
                    Notification.Logger(Constants.AssemblyName, "Missing or incorrectly installed Scatterer.");

                    Notification.Dialog("Missing Scatterer", Constants.AssemblyName + " requires the Scatterer mod in order to function properly.\n");
                }

                //  Check it Module Manager is installed.

                if (!AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName ().Name.StartsWith ("ModuleManager", StringComparison.InvariantCultureIgnoreCase) && a.url.ToLower() == ""))
                {
                    Notification.Logger(Constants.AssemblyName, "Missing or incorrectly installed Module Manager.");

                    Notification.Dialog("Missing Module Manager", Constants.AssemblyName + " requires the Module Manager mod in order to function properly.\n");
                }
            }
            catch (Exception ex)
            {
                Notification.Logger(Constants.AssemblyName, "Caught an exception: \n" + ex.Message + "\n" + ex.StackTrace);

                Notification.Dialog("Incorrect " + Constants.AssemblyName + " installation",
                    "An error has occurred while checking the installation of " + Constants.AssemblyName + ".\n\n" +
                    "You need to\n" +
                    "  • Terminate the KSP instance\n" +
                    "  • Send a complete copy of the 'output.log' file to the mod developer\n" +
                    "  • Completely remove and re-install " + Constants.AssemblyName);
            }
        }
    }
}
