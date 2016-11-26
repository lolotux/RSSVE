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

namespace RSSVE
{
    public static class Constants
    {
        public struct VersionCompatible
        {
            static public readonly int Major = 1;
            static public readonly int Minor = 2;
            static public readonly int Revis = 1; 
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
                if (_AssemblyVersion == null)
                {
                    var GetVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    _AssemblyVersion = GetVersion.Major + "." + GetVersion.Minor + "." + GetVersion.Revision + "." + GetVersion.Build;
                }

                return _AssemblyVersion;
            }
        }
    }
}
