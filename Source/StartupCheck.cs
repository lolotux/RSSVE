//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright Â© 2016-2018, Alexander "Phineas Freak" Kampolis.

//  This file is part of Real Solar System Visual Enhancements.

//  Real Solar System Visual Enhancements is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0
//  (CC-BY-NC-SA 4.0) license.

//  You should have received a copy of the license along with this work. If not, visit the official
//  Creative Commons web page:

//      â€¢ https://www.creativecommons.org/licensies/by-nc-sa/4.0
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

        void Start ()
        {
            //  Log some basic information that might be of interest when debugging installations.

            Notification.Logger (Constants.AssemblyName, null, string.Format ("Assembly location: {0}", Assembly.GetExecutingAssembly ().Location));
            Notification.Logger (Constants.AssemblyName, null, string.Format ("Assembly version: {0}", Version.GetAssemblyVersion));
            Notification.Logger (Constants.AssemblyName, null, string.Format ("Assembly compatibility: {0}.{1}.{2}.{3}", Constants.VersionCompatible.Major, Constants.VersionCompatible.Minor, Constants.VersionCompatible.Revis, Constants.VersionCompatible.Build));

            //  The following information fields are only active if the "Verbose Logging" option in the KSP menu is checked.

            if (GameSettings.VERBOSE_DEBUG_LOG.Equals (true))
            {
                Notification.Logger (Constants.AssemblyName, null, string.Format ("Using x86-64 KSP binaries: {0}", Utilities.Is64BitOS));
                Notification.Logger (Constants.AssemblyName, null, string.Format ("Using Unity player: {0}", Utilities.GetPlatformType));
                Notification.Logger (Constants.AssemblyName, null, string.Format ("Using renderer: {0}", Utilities.GetGraphicsRenderer));
            }

            //  Check if we are running under a x86 environment. The large amount of RAM space required by the RSSVE assets does not
            //  allow the use of the 32 bit binaries.

            if (Utilities.Is64BitOS.Equals (false))
            {
                Notification.Dialog ("OSChecker", "Unsupported OS Version", "#F0F0F0", string.Format ("{0} is not supported by 32 bit KSP installations.\n\nPlease use the 64 bit instance of KSP.", Constants.AssemblyName), "#F0F0F0");

                Notification.Logger (Constants.AssemblyName, "Error", "Unsupported OS Version (using 32 bit)!");
            }

            //  Check if we are using a different graphics renderer (D3D11 or OpenGL) under Windows (should be using D3D9 for now).
            //  MacOS and Linux do not have the same problem as they only use the OpenGL renderer.

            if (Utilities.GetPlatformType.Equals ("Windows") && !Utilities.GetGraphicsRenderer.Equals ("D3D9"))
            {
                string IncompatibleRendererMsg = string.Empty;

                switch (Utilities.GetGraphicsRenderer)
                {
                    case ("D3D11"):

                        IncompatibleRendererMsg = "  •  DirectX 11";

                    break;

                    case ("OpenGL"):

                        IncompatibleRendererMsg = "  •  OpenGL";

                    break;
                }

                Notification.Dialog ("RendererChecker", "Unsupported Graphics Renderer Detected", "#F0F0F0", string.Format ("The following graphics renderer is unsupported by {0} under the Windows OS:\n\n{1}\n\n Please use the default DirectX 9 graphics renderer.", Constants.AssemblyName, IncompatibleRendererMsg), "#F0F0F0");

                Notification.Logger (Constants.AssemblyName, "Warning", string.Format ("Unsupported Graphics Renderer (using {0})!", Utilities.GetGraphicsRenderer));
            }
        }
    }
}
