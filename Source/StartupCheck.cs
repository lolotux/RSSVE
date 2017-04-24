//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016-2017, Alexander "Phineas Freak" Kampolis.

//  This file is part of Real Solar System Visual Enhancements.

//  Real Solar System Visual Enhancements is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0
//  (CC-BY-NC-SA 4.0) license.

//  You should have received a copy of the license along with this work. If not, visit the official
//  Creative Commons web page:

//      • https://www.creativecommons.org/licensies/by-nc-sa/4.0
//  ================================================================================

using System.Reflection;
using UnityEngine;

namespace RSSVE
{
    /// <summary>
    /// Startup System checker. Operates only in the Loading scene.
    /// </summary>

    [KSPAddon (KSPAddon.Startup.Instantly, true)]

    class StartupChecker : MonoBehaviour
    {
        /// <summary>
        /// Method to start the startup checker.
        /// </summary>

        public void Start ()
        {
            //  Log some basic information that might be of interest when debugging installations.

            Notification.Logger (Constants.AssemblyName, null, string.Format ("Assembly location: {0}", Assembly.GetExecutingAssembly ().Location));
            Notification.Logger (Constants.AssemblyName, null, string.Format ("Assembly version: {0}", Version.GetAssemblyVersion));
            Notification.Logger (Constants.AssemblyName, null, string.Format ("Assembly compatibility: {0}.{1}.{2}.{3}", Constants.VersionCompatible.Major, Constants.VersionCompatible.Minor, Constants.VersionCompatible.Revis, Constants.VersionCompatible.Build));

            #if DEBUG
            {
                Notification.Logger (Constants.AssemblyName, null, string.Format ("Using x86-64 binaries: {0}", System.Is64BitOS));
                Notification.Logger (Constants.AssemblyName, null, string.Format ("Using Unity player: {0}", System.GetPlatformType));
            }
            #endif

            //  Check if we are running under a x86 environment. The large amount of memory space required by the texture assets does not
            //  allow the use of the 32 bit binaries.

            if (System.Is64BitOS.Equals (false))
            {
                Notification.Dialog ("Unsupported OS Version", string.Format ("{0} does not operate correctly under 32 bit KSP installations.\n\nPlease use the 64 bit instance of KSP.", Constants.AssemblyName));

                Notification.Logger (Constants.AssemblyName, "Error", "Unsupported OS Version (using 32 bit)!");
            }

            //  Check if we are using a different graphics renderer (D3D11 or OpenGL) under Windows (should use D3D9 for now).
            //  MacOS and Linux do not have the same problem as they only use the OpenGL renderer.

            if (System.GetPlatformType.Equals ("Windows") && (!System.GetGraphicsRenderer.Equals ("D3D9")))
            {
                string IncompatibleRendererMsg = string.Empty;

                switch (System.GetGraphicsRenderer)
                {
                    case ("D3D11"):

                        IncompatibleRendererMsg = "  •  DirectX 11";

                    break;

                    case ("OpenGL"):

                        IncompatibleRendererMsg = "  •  OpenGL";

                    break;
                }

                Notification.Dialog ("Unsupported Graphics Renderer Detected", string.Format ("The following listed graphics renderer is unsupported by {0} under the Windows OS:\n\n{1}\n\n Please use the default DirectX 9 graphics renderer.", Constants.AssemblyName, IncompatibleRendererMsg));

                Notification.Logger (Constants.AssemblyName, "Error", string.Format ("Unsupported Graphics Renderer (using {0})!", System.GetGraphicsRenderer));
            }
        }
    }
}
