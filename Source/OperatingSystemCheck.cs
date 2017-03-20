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

using UnityEngine;

namespace RSSVE
{
    /// <summary>
    /// Operating System checker. Operates only in the Loading scene.
    /// </summary>

    [KSPAddon(KSPAddon.Startup.Instantly, true)]

    class OSChecker : MonoBehaviour
    {
        /// <summary>
        /// Method to start the OS checker.
        /// </summary>

        public void Start()
        {
            //  Check if we are running under a x86 environment and warn the user.

            if (System.Is64BitOS () == "No")
            {
                Notification.Dialog ("Incompatible OS Version", Constants.AssemblyName + " will not operate correctly under 32 bit KSP installations. Please use the 64 bit instance of KSP.");
            }
        }
    }
}
