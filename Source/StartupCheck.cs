//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016-2018, Alexander "Phineas Freak" Kampolis.

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
        }
    }
}
