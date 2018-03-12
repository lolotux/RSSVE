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

using System;
using System.Collections.Generic;

namespace RSSVE
{
    /// <summary>
    /// Environmental Visual Enhancements configuration file integrity checker class.
    /// </summary>

    static class EVEConfigChecker
    {
        /// <summary>
        /// Method to validate an EVE configuration file.
        /// </summary>
        /// <param name = "EVENodeToCheck">The name of the configuration file to be checked.</param>
        /// <returns>
        /// Does not return a value.
        /// </returns>

        public static void ValidateConfig (string EVENodeToCheck)
        {
            if (!String.IsNullOrEmpty (EVENodeToCheck))
            {
                //  Check if other EVE configs are present in the GameDatabase
                //  (configs that refer to non-existent bodies WILL break EVE).

                string PrevBodyName = string.Empty;

                var BodyLoaderNames = new List <string>();

                int EVENodeCount = 0;

                //  Start by creating a list with all possible celestial bodies found in the GameDatabase.

                foreach (var Celestial in FlightGlobals.Bodies)
                {
                    //  Print the body name to the log (for debug purposes).

                    if (GameSettings.VERBOSE_DEBUG_LOG.Equals (true))
                    {
                        Notification.Logger (Constants.AssemblyName, "", string.Format ("CelestialBody found: {0}", Celestial.bodyName));
                    }

                    //  Add the body name to the list. We are saving them as lowercase since it is the
                    //  least common.

                    BodyLoaderNames.Add (Celestial.bodyName.ToLower ());
                }

                //  Scan the GameDatabase for all loaded EVE configs.

                foreach (ConfigNode EVENode in GameDatabase.Instance.GetConfigNodes (EVENodeToCheck))
                {
                    EVENodeCount++;

                    //  Search for the EVE body names;

                    foreach (ConfigNode EVECloudsObject in EVENode.GetNodes ("OBJECT"))
                    {
                        if (EVENode != null && EVECloudsObject.HasValue ("body"))
                        {
                            // Get the EVE_CLOUDS body name.

                            string BodyName = EVECloudsObject.GetValue ("body");

                            //  Check if the body name exists in the body name database. If not then we are going to have
                            //  a bad time...

                            if (Array.IndexOf (BodyLoaderNames.ToArray (), BodyName.ToLower ()) < 0)
                            {
                                //  Print the invalid body name (for debug purposes).
                                //  Also, make sure that we are printing the body name only once (EVE
                                //  nodes may contain multiple objects for the same body).

                                if (GameSettings.VERBOSE_DEBUG_LOG.Equals (true) && !BodyName.Equals (PrevBodyName))
                                {
                                    PrevBodyName = BodyName;

                                    Notification.Logger (Constants.AssemblyName, "", string.Format ("Incompatible {0} body detected! (name: {1})", EVENodeToCheck, BodyName));
                                }

                                //  Remove the invalid EVE config from the GameDatabase.

                                EVENode.ClearNodes ();
                            }
                        }
                    }
                }

                //  Print the total number of EVE configs loaded (for debug purposes).

                if (GameSettings.VERBOSE_DEBUG_LOG.Equals (true))
                {
                    Notification.Logger (Constants.AssemblyName, "", string.Format ("{0} config detected: (count: {1})", EVENodeToCheck, EVENodeCount));
                }
            }
        }
    }
}
