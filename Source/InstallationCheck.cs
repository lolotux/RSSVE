//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016, Phineas Freak

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
        protected void Start()
        {
            try
            {
                //  Log some basic information that might be of interest when debugging.

                Debug.Log("[" + Constants.AssemblyName + "] Assembly location: " + Assembly.GetExecutingAssembly().Location);
                Debug.Log("[" + Constants.AssemblyName + "] Assembly version: " + Version.AssemblyVersion);
                Debug.Log("[" + Constants.AssemblyName + "] Assembly compatibility: " + Constants.VersionCompatible.Major + "." + Constants.VersionCompatible.Minor  + "." + Constants.VersionCompatible.Revis);

                //  Search for this mod's DLL existing in the wrong location. This will also detect duplicate copies because only one can be in the right place.

                var assemblies = AssemblyLoader.loadedAssemblies.Where(a => a.assembly.GetName().Name == Assembly.GetExecutingAssembly().GetName().Name).Where(a => a.url != Constants.AssemblyPath);

                if (assemblies.Any())
                {
                    var badPaths = assemblies.Select(a => a.path).Select(p => Uri.UnescapeDataString(new Uri(Path.GetFullPath(KSPUtil.ApplicationRootPath)).MakeRelativeUri(new Uri(p)).ToString().Replace('/', Path.DirectorySeparatorChar)));

                    string badPathsString = string.Join("\n", badPaths.ToArray());

                    Debug.Log("[" + Constants.AssemblyName + "] Incorrect installation, bad path(s):\n" + badPathsString);

                    PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), "Incorrect " + Constants.AssemblyName + " Installation", Constants.AssemblyName + " has been installed incorrectly and will not function properly. All files should be located in KSP/GameData/" + Constants.AssemblyName + ". Do not move any files from inside that folder.\n\nIncorrect path(s):\n" + badPathsString, "OK", false, HighLogic.UISkin);
                }

                //  Check if Environmental Visual Enhancements is installed.

                if (!AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName ().Name.StartsWith ("EVEManager", StringComparison.CurrentCulture) && a.url == ""))
                {
                    Debug.Log("[" + Constants.AssemblyName + "] Missing or incorrectly installed Environmental Visual Enhancements.");

                    PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), "Missing Environmental Visual Enhancements", Constants.AssemblyName + " requires the Environmental Visual Enhancements mod in order to function properly.\n", "OK", false, HighLogic.UISkin);
                }

                //  Check if Scatterer is installed.

                if (!AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName ().Name.StartsWith ("scatterer", StringComparison.CurrentCulture) && a.url == ""))
                {
                    Debug.Log("[" + Constants.AssemblyName + "] Missing or incorrectly installed Scatterer.");

                    PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), "Missing Scatterer", Constants.AssemblyName + " requires the Scatterer mod in order to function properly.\n", "OK", false, HighLogic.UISkin);
                }

                //  Check it Module Manager is installed.

                if (!AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName ().Name.StartsWith ("ModuleManager", StringComparison.CurrentCulture) && a.url == ""))
                {
                    Debug.Log("[" + Constants.AssemblyName + "] Missing or incorrectly installed Module Manager.");

                    PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), "Missing Module Manager", Constants.AssemblyName + " requires the Module Manager mod in order to function properly.\n", "OK", false, HighLogic.UISkin);
                }
            }
            catch (Exception ex)
            {
                Debug.Log("[" + Constants.AssemblyName + "] Caught an exception: \n" + ex.Message + "\n" + ex.StackTrace);

                PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), "Incorrect " + Constants.AssemblyName + " installation",
                    "An error has occurred while checking the installation of " + Constants.AssemblyName + ".\n\n" +
                    "You need to\n" +
                    "  • Terminate the KSP instance\n" +
                    "  • Send a complete copy of the 'output.log' file to the mod developer\n" +
                    "  • Completely remove and re-install " + Constants.AssemblyName,
                    "OK", false, HighLogic.UISkin);
            }
        }
    }
}
