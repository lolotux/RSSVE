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
        /// The (relative to the "GameData") path where the assembly resides.
        /// </summary>

        static public readonly string AssemblyPath = AssemblyName + "/Plugins";
    }

    /// <summary>
    /// Class to create user notification dialogs and log basic information to the KSP log file.
    /// </summary>

    class Notification
    {
        /// <summary>
        /// Method to create pop-up notification dialogs.
        /// </summary>
        /// <param name = "TitleText">The title text of the dialog window. (string)</param>
        /// <param name = "TitleColor">The color of the message text.</param>
        /// <param name = "ContentText">The message text of the dialog window. (string)</param>
        /// <param name = "ContentColor">The color of the message text.</param>
        /// <returns>
        /// Does not return a value.
        /// </returns>

        public static void Dialog (string TitleText, string TitleColor, string ContentText, string ContentColor)
        {
            if (!TitleText.Equals (null) && (!ContentText.Equals (null)))
            {
                PopupDialog.SpawnPopupDialog (new Vector2 (0.5f, 0.5f), new Vector2 (0.5f, 0.5f), string.Format ("<color={0}>{1}</color>", TitleColor, TitleText), string.Format ("<color={0}>{1}</color>", ContentColor, ContentText), "OK", false, HighLogic.UISkin, true, string.Empty);
            }
        }

        /// <summary>
        /// Method to print generic text in the KSP debug log.
        /// </summary>
        /// <param name = "AssemblyTagName">Name of the assembly (to be used as a log tag) (string).</param>
        /// <param name = "LogType">The type of the log. Can be one one of the following: null (for the basic "Log" type), Warning or Error (string).</param>
        /// <param name = "Content">The message to be logged (string).</param>
        /// <returns>
        /// Does not return a value.
        /// </returns>

        public static void Logger (string AssemblyTagName, string LogType, string Content)
        {
            if (!AssemblyTagName.Equals (null))
            {
                switch (LogType)
                {
                    case ("Warning"):

                        UnityEngine.Debug.LogWarning (string.Format ("[{0}]: {1}", AssemblyTagName, Content));

                    break;

                    case ("Error"):

                        UnityEngine.Debug.LogError (string.Format ("[{0}]: {1}", AssemblyTagName, Content));

                    break;

                    default:

                        UnityEngine.Debug.Log (string.Format ("[{0}]: {1}", AssemblyTagName, Content));

                    break;
                }
            }
        }
    }

    /// <summary>
    /// Class to get basic system operational parameters.
    /// </summary>

    public static class System
    {
        /// <summary>
        /// Method to get the operating system graphics renderer.
        /// </summary>
        /// <returns>
        /// Returns one of the following renderer types: D3D9, D3D11, OpenGL or Unknown.
        /// </returns>

        public static string GetGraphicsRenderer
        {
            get
            {
                var RendererType = SystemInfo.graphicsDeviceVersion;

                string RendererName = string.Empty;

                if (RendererType.StartsWith ("Direct3D 9", StringComparison.InvariantCultureIgnoreCase))
                {
                    RendererName = "D3D9";
                }
                else if (RendererType.StartsWith ("Direct3D 11", StringComparison.InvariantCultureIgnoreCase))
                {
                    RendererName = "D3D11";
                }
                else if (RendererType.StartsWith ("OpenGL", StringComparison.InvariantCultureIgnoreCase))
                {
                    RendererName = "OpenGL";
                }
                else
                {
                    RendererName = "Unknown";
                }

                return RendererName;
            }
        }

        /// <summary>
        /// Method to get the operating system octet size.
        /// </summary>
        /// <returns>
        /// Returns "True" if the Operating System is using the AMD64 specification (x64) and "False" if it is using the baseline Intel specification (x86).
        /// </returns>

        public static bool Is64BitOS
        {
            get
            {
                if (IntPtr.Size.Equals (8))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Method to get the operating system type.
        /// </summary>
        /// <returns>
        /// Returns one the following operating system types: Linux, OSX, Windows or Unknown.
        /// </returns>

        public static string GetPlatformType
        {
            get
            {
                var PlatformType = Application.platform;

                string PlatformTypeName = string.Empty;

                switch (PlatformType)
                {
                    case RuntimePlatform.LinuxPlayer:

                        PlatformTypeName = "Linux";

                    break;

                    case RuntimePlatform.OSXPlayer:

                        PlatformTypeName = "OSX";

                    break;

                    case RuntimePlatform.WindowsPlayer:

                        PlatformTypeName = "Windows";

                    break;

                    default:

                        PlatformTypeName = "Unknown";

                    break;
                }

                return PlatformTypeName;
            }
        }
    }

    /// <summary>
    /// Class to get the assembly version information.
    /// </summary>

    class Version
    {
        /// <summary>
        /// Method to get the assembly version.
        /// </summary>
        /// <returns>
        /// Returns the assembly version as a string (major.minor.revision.build).
        /// </returns>

        public static string GetAssemblyVersion
        {
            get
            {
                var AssemblyVersion = FileVersionInfo.GetVersionInfo (Assembly.GetExecutingAssembly ().Location);

                return string.Format ("{0}.{1}.{2}.{3}", AssemblyVersion.FileMajorPart, AssemblyVersion.FileMinorPart, AssemblyVersion.FileBuildPart, AssemblyVersion.FilePrivatePart);
            }
        }
    }
}
