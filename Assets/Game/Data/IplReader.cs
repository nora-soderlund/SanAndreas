using System;
using System.Linq;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;

namespace Game.Data {
    /// <summary>
    /// Item placement files are map files used to place objects into the world,
    /// as well as define zones, paths, garages, interior portals, and a lot more. 
    /// </summary>
    public class IplReader : DataReader
    {
        public IplReader(string file) : base(file) {

        }

        #region Public methods
        public bool ReadInst(out IplInst inst) {
            inst = null;

            if(this.section != "inst") {
                if(!this.readSection("inst"))
                    return false;
            }

            string line = this.reader.ReadLine();

            if(line.ToLower() == "end") {
                this.section = string.Empty;

                return false;
            }

            string[] subsections = line.Split(',').Select(x => x.Trim()).ToArray();

            inst = new IplInst() {
                ID = int.Parse(subsections[0]),

                ModelName = subsections[1],
                
                Interior = int.Parse(subsections[2]),

                Position = new Vector3(float.Parse(subsections[3]), float.Parse(subsections[5]), float.Parse(subsections[4])),
                Rotation = new Quaternion(float.Parse(subsections[6]), float.Parse(subsections[8]), float.Parse(subsections[7]), float.Parse(subsections[9])) * Quaternion.Euler(-90f, 180f, 0),
                
                LOD = int.Parse(subsections[10]),
            };

            return true;
        }

        public bool ReadCull(out IplCull cull) {
            cull = null;

            if(this.section != "cull") {
                if(!this.readSection("cull"))
                    return false;
            }

            string line = this.reader.ReadLine();

            if(line.ToLower() == "end") {
                this.section = string.Empty;

                return false;
            }

            string[] subsections = line.Split(',').Select(x => x.Trim()).ToArray();

            if(subsections.Length == 11) {
                // CenterX, CenterY, CenterZ, Unknown1, WidthY, BottomZ, WidthX, Unknown2, TopZ, Flag, Unknown3

                throw new NotImplementedException();
            }
            else if(subsections.Length == 14) {
                // CenterX, CenterY, CenterZ, Unknown1, WidthY, BottomZ, WidthX, Unknown2, TopZ, Flag, Vx, Vy, Vz, Cm

                throw new NotImplementedException();
            }
            
            return false;
        }
        #endregion
    }
}
