//  ================================================================================
//  Real Solar System Visual Enhancements for Kerbal Space Program.

//  Copyright © 2016, Phineas Freak

//  This file is part of Real Solar System Visual Enhancements.

//  Real Solar System Visual Enhancements is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0
//  (CC-BY-NC-SA 4.0) license.

//  You should have received a copy of the license along with this work. If not, visit the official
//  Creative Commons web page:

//      • https://www.creativecommons.org/licensies/by-nc-sa/4.0
//  ================================================================================

using System.Reflection;
using System.Diagnostics;

namespace RSSVE
{
    public static class Constants
    {
        public struct VersionCompatible
        {
            static public int Major = 1;
            static public int Minor = 2;
            static public int Revis = 1; 
        }

        static public readonly string AssemblyName = "RSSVE";
        static public readonly string AssemblyPath =  AssemblyName + "/Plugins";
    }

    class Version
    {
        static public string _AssemblyVersion;

        static public string AssemblyVersion
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
