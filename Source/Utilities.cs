//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016, Alexander "Phineas Freak" Kampolis

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
    public static class Constants
    {
        public struct VersionCompatible
        {
            static public readonly int Major = 1;
            static public readonly int Minor = 2;
            static public readonly int Revis = 2;
        }

        static public readonly string UnityVersion = "5.4.0p4";
        static public readonly string AssemblyName = "RSSVE";
        static public readonly string AssemblyPath =  AssemblyName + "/Plugins";
    }

    class Notification
    {
        public static bool Dialog(string MessageTitle, string MessageContent)
        {
            PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), MessageTitle, MessageContent, "OK", false, HighLogic.UISkin);

            return true;
        }

        public static bool Logger(string AssemblyTagName, string Content)
        {
            UnityEngine.Debug.Log("[" + AssemblyTagName + "] " + Content);

            return true;
        }
    }

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
