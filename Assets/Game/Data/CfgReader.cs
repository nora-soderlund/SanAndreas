using System;
using System.Linq;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;

namespace Game.Data {
    public class CfgReader : IDisposable {
        internal StreamReader reader;


        public CfgReader(string file) {
            this.reader = new StreamReader(file);
        }

        public bool TryReadLine(out string line) {
            line = null;

            string read = this.reader.ReadLine();

            while(read.StartsWith(";") && !this.reader.EndOfStream)
                read = this.reader.ReadLine();

            if(this.reader.EndOfStream)
                return false;

            line = read;

            return true;
        }

        public bool TryReadHandling(out CfgHandling cfgHandling) {
            cfgHandling = null;

            if(!this.TryReadLine(out string line))
                return false;

            // For Boats: (some car handling variables are used for alternate functions in boats)
            if(line.StartsWith("%")) {
                
            }
            // '!' identifies this line as bike data for reading
            else if(line.StartsWith("!")) {
                
            }
            // '$' identifies this line as flying handling data when loading
            else if(line.StartsWith("$")) {
                
            }
            // '^' identifies this line as vehicle anim group data for reading
            else if(line.StartsWith("^")) {
                
            }
            // generic vehicle!
            else {

                string[] sections = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                cfgHandling = new CfgHandling();
                
                cfgHandling.HandlingId = sections[0].Trim();

                cfgHandling.Mass = float.Parse(sections[1].Trim());
                cfgHandling.TurnMass = float.Parse(sections[2].Trim());
                cfgHandling.DragMult = float.Parse(sections[3].Trim());
                
                // sections[4] unused
                cfgHandling.CentreOfMass = new Vector3(
                    float.Parse(sections[4].Trim()),
                    float.Parse(sections[6].Trim()),
                    float.Parse(sections[5].Trim())
                );

                cfgHandling.PercentSubmerged = int.Parse(sections[7].Trim());
                
                cfgHandling.TractionMultiplier = float.Parse(sections[8].Trim());
                cfgHandling.TractionLoss = float.Parse(sections[9].Trim());
                cfgHandling.TractionBias = float.Parse(sections[10].Trim());

                cfgHandling.NumberOfGears = int.Parse(sections[11].Trim());
                cfgHandling.MaxVelocity = float.Parse(sections[12].Trim());
                cfgHandling.EngineAcceleration = float.Parse(sections[13].Trim());
                cfgHandling.EngineInertia = float.Parse(sections[14].Trim());
                cfgHandling.DriveType = char.Parse(sections[15].Trim());
                cfgHandling.EngineType = char.Parse(sections[16].Trim());
                cfgHandling.BrakeDeceleration = float.Parse(sections[17].Trim());
                cfgHandling.BrakeBias = float.Parse(sections[18].Trim());
                cfgHandling.ABS = int.Parse(sections[19].Trim()) == 1;
                cfgHandling.SteeringLock = float.Parse(sections[20].Trim());

                cfgHandling.SuspensionForceLevel = float.Parse(sections[21].Trim());
                cfgHandling.SuspensionDamingLevel = float.Parse(sections[22].Trim());
                cfgHandling.SuspensionHighSpeedComDamp = float.Parse(sections[23].Trim());
                cfgHandling.SuspensionUpperLimit = float.Parse(sections[24].Trim());
                cfgHandling.SuspensionLowerLimit = float.Parse(sections[25].Trim());
                cfgHandling.SuspensionBiasBetweenFrontAndRear = float.Parse(sections[26].Trim());
                cfgHandling.SuspensionAntiDiveMultiplier = float.Parse(sections[27].Trim());

                cfgHandling.SeatOffsetDistance = float.Parse(sections[28].Trim());
                cfgHandling.CollisionDamageMultiplier = float.Parse(sections[29].Trim());
                cfgHandling.MonetaryValue = int.Parse(sections[30].Trim());

                cfgHandling.ModelFlags = sections[31].Trim();
                cfgHandling.HandlingFlags = sections[32].Trim();

                cfgHandling.FrontLights = int.Parse(sections[33].Trim());
                cfgHandling.RearLights = int.Parse(sections[34].Trim());
                cfgHandling.AnimationGroup = int.Parse(sections[35].Trim());
            }

            return (cfgHandling != null);
        }

        public void Dispose() {
            reader.Dispose();
        }
    }
}
