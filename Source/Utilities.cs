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
using System.Diagnostics;
using UnityEngine;

namespace RSSVE
{
    /// <summary>
    /// Set up the constants used by RSSVE.
    /// </summary>

    public static class Constants
    {
        /// <summary>
        /// Version compatible struct.
        /// </summary>

        public struct VersionCompatible
        {
            /// <summary>
            /// The major version value.
            /// </summary>

            static public readonly int Major = 1;

            /// <summary>
            /// The minor version value.
            /// </summary>

            static public readonly int Minor = 2;

            /// <summary>
            /// The revision version value.
            /// </summary>

            static public readonly int Revis = 2;
        }

        /// <summary>
        /// The compatible Unity version.
        /// </summary>

        static public readonly string UnityVersion = "5.4.0p4";

        /// <summary>
        /// The name of the assembly (used as a tag).
        /// </summary>

        static public readonly string AssemblyName = "RSSVE";

        /// <summary>
        /// The relative path where the assembly resides.
        /// </summary>

        static public readonly string AssemblyPath =  AssemblyName + "/Plugins";
    }

    class Notification
    {
        /// <summary>
        /// Method to create popup notification dialogs.
        /// </summary>
        /// <param name = "MessageTitle">Dialog title (string)</param>
        /// <param name = "MessageContent">Dialog message (string)</param>
        /// <returns>
        /// Returns always true.
        /// </returns>

        public static bool Dialog(string MessageTitle, string MessageContent)
        {
            PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), MessageTitle, MessageContent, "OK", false, HighLogic.UISkin);

            return true;
        }

        /// <summary>
        /// Method to print generic text to the KSP debug log.
        /// </summary>
        /// <param name = "AssemblyTagName">Assembly tag (string)</param>
        /// <param name = "Content">Log message (string)</param>
        /// <returns>
        /// Returns always true.
        /// </returns>

        public static bool Logger(string AssemblyTagName, string Content)
        {
            UnityEngine.Debug.Log("[" + AssemblyTagName + "] " + Content);

            return true;
        }
    }

    /// <summary>
    /// Method to get the assembly version.
    /// </summary>
    /// <returns>
    /// Returns the assembly version as a string (major.minor.revision.build).
    /// </returns>

    class Version
    {
        public static string _AssemblyVersion;

        public static string AssemblyVersion
        {
            get
            {
                var GetVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

                _AssemblyVersion = GetVersion.FileMajorPart + "." + GetVersion.FileMinorPart + "." + GetVersion.FileBuildPart + "." + GetVersion.FilePrivatePart;

                return _AssemblyVersion;
            }
        }
    }
}
