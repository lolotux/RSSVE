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

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace RSSVE
{
    /// <summary>
    /// Class to set up the parameters required and used by RSSVE.
    /// </summary>

    public static class Constants
    {
        /// <summary>
        /// Version parameters struct.
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

            /// <summary>
            /// The build version value.
            /// </summary>

            static public readonly int Build = 1622;
        }

        /// <summary>
        /// The compatible Unity version.
        /// </summary>

        static public readonly string UnityVersion = "5.4.0p4";

        /// <summary>
        /// The name of the assembly (used as a tag for the notification dialogs and the log file).
        /// </summary>

        static public readonly string AssemblyName = "RSSVE";

        /// <summary>
        /// The relative path where the assembly resides.
        /// </summary>

        static public readonly string AssemblyPath =  AssemblyName + Path.DirectorySeparatorChar + "Plugins";
    }

    /// <summary>
    /// Class to create user notification dialogs and log basic information to the KSP log file.
    /// </summary>

    class Notification
    {
        /// <summary>
        /// Method to create popup notification dialogs.
        /// </summary>
        /// <param name = "MessageTitle">Dialog title (string)</param>
        /// <param name = "MessageContent">Dialog message (string)</param>
        /// <returns>
        /// Does not return a value.
        /// </returns>

        public static void Dialog(string MessageTitle, string MessageContent)
        {
            PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), MessageTitle, MessageContent, "OK", false, HighLogic.UISkin);
        }

        /// <summary>
        /// Method to print generic text in the KSP debug log.
        /// </summary>
        /// <param name = "AssemblyTagName">Assembly tag (string)</param>
        /// <param name = "Content">Log message (string)</param>
        /// <returns>
        /// Does not return a value.
        /// </returns>

        public static void Logger(string AssemblyTagName, string Content)
        {
            UnityEngine.Debug.Log(string.Format("[{0}]: {1}", AssemblyTagName, Content));
        }
    }

    /// <summary>
    /// Class to get basic system operational parameters.
    /// </summary>

    public static class System
    {
        /// <summary>
        /// Method to get the operating system octet size.
        /// </summary>
        /// <returns>
        /// Returns "Yes" if the Operating System is using the AMD64 specification (x64) and "No" if it is using the baseline x86.
        /// </returns>

        public static string Is64BitOS()
        {
            if (IntPtr.Size == 8)
            {
                return "Yes";
            }

            return "No";
        }
    }    

    /// <summary>
    /// Class to get the assembly version information.
    /// </summary>

    class Version
    {
        public static string _AssemblyVersion;

        /// <summary>
        /// Method to get the assembly version.
        /// </summary>
        /// <returns>
        /// Returns the assembly version as a string (major.minor.revision.build).
        /// </returns>

        public static string AssemblyVersion
        {
            get
            {
                var GetVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

                _AssemblyVersion = string.Format("{0}.{1}.{2}.{3}", GetVersion.FileMajorPart, GetVersion.FileMinorPart, GetVersion.FileBuildPart, GetVersion.FilePrivatePart);

                return _AssemblyVersion;
            }
        }
    }
}
