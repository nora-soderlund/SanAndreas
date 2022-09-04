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
    public class IdeReader : DataReader
    {
        public IdeReader(string file) : base(file) {
            
        }

        #region Public methods
        public bool ReadObj(out IdeObj obj) {
            obj = null;

            if(this.section != "objs") {
                if(!this.readSection("objs"))
                    return false;
            }

            string line = this.reader.ReadLine();

            if(line.ToLower() == "end") {
                this.section = string.Empty;

                return false;
            }

            string[] subsections = line.Split(',').Select(x => x.Trim()).ToArray();

            if(subsections.Length >= 6) {
                // ID, ModelName, TextureName, ObjectCount, DrawDist, [DrawDist2, ...], Flags
                
                List<float> drawDistances = new List<float>();

                for(int index = 4; index < subsections.Length - 1; index++)
                    drawDistances.Add(float.Parse(subsections[index]));

                obj = new IdeObj() {
                    ID = int.Parse(subsections[0]),
                    ModelName = subsections[1],
                    TextureName = subsections[2],
                    ObjectCount = int.Parse(subsections[3]),
                    DrawDistances = drawDistances.ToArray(),
                    Flags = (IdeObjFlags)int.Parse(subsections[subsections.Length - 1])
                };
            }
            else {
                // ID, ModelName, TextureName, DrawDist, [DrawDist2, ...], Flags
                
                List<float> drawDistances = new List<float>();

                for(int index = 3; index < subsections.Length - 1; index++)
                    drawDistances.Add(float.Parse(subsections[index]));

                obj = new IdeObj() {
                    ID = int.Parse(subsections[0]),
                    ModelName = subsections[1],
                    TextureName = subsections[2],
                    DrawDistances = drawDistances.ToArray(),
                    Flags = (IdeObjFlags)int.Parse(subsections[subsections.Length - 1])
                };
            }

            return true;
        }

        public bool ReadCars(out IdeCar ideCar) {
            ideCar = null;

            if(this.section != "cars") {
                if(!this.readSection("cars"))
                    return false;
            }

            string line = this.reader.ReadLine();

            while((line.Length == 0 || line.StartsWith("#")) && !this.reader.EndOfStream)
                line = this.reader.ReadLine();

            if(line.Length == 0 || line.StartsWith("#") || this.reader.EndOfStream)
                return false;

            if(line.ToLower() == "end") {
                this.section = string.Empty;

                return false;
            }

            string[] sections = line.Split(',');

            ideCar = new IdeCar() {
                ModelId = int.Parse(sections[0].Trim()),
                ModelName = sections[1].Trim(),
                TextureName = sections[2].Trim(),
                Type = sections[3].Trim(),
                HandlingId = sections[4].Trim(),
                GameName = sections[5].Trim(),
                Anims = sections[6].Trim(),
                Class = sections[7].Trim(),
                Frequency = int.Parse(sections[8].Trim()),
                Flags = int.Parse(sections[9].Trim())
            };

            int.TryParse(sections[10].Trim(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture, out ideCar.CompRules);

            if(sections.Length == 15) {
                ideCar.WheelId = int.Parse(sections[11].Trim());
                ideCar.WheelScale_Front = float.Parse(sections[12].Trim());
                ideCar.WheelScale_Rear = float.Parse(sections[13].Trim());
                ideCar.WheelUpgradeClass = int.Parse(sections[14].Trim());
            }

            return true;
        }

        public bool ReadTObj(out IdeTObj obj) {
            obj = null;

            if(this.section != "tobj") {
                if(!this.readSection("tobj"))
                    return false;
            }

            string line = this.reader.ReadLine();

            if(line.ToLower() == "end") {
                this.section = string.Empty;

                return false;
            }

            string[] subsections = line.Split(',').Select(x => x.Trim()).ToArray();

            if(subsections[3].Contains('.')) {
                // ID, ModelName, TextureName, ObjectCount, DrawDist, [DrawDist2, ...], Flags, TimeOn, TimeOff
                
                List<float> drawDistances = new List<float>();

                for(int index = 4; index < subsections.Length - 1; index++)
                    drawDistances.Add(float.Parse(subsections[index]));

                obj = new IdeTObj() {
                    ID = int.Parse(subsections[0]),
                    ModelName = subsections[1],
                    TextureName = subsections[2],
                    ObjectCount = int.Parse(subsections[3]),
                    DrawDistances = drawDistances.ToArray(),
                    Flags = (IdeObjFlags)int.Parse(subsections[subsections.Length - 3]),
                    TimeOn = int.Parse(subsections[subsections.Length - 2]),
                    TimeOff = int.Parse(subsections[subsections.Length - 1])
                };
            }
            else {
                // ID, ModelName, TextureName, DrawDist, [DrawDist2, ...], Flags, TimeOn, TimeOff
                
                List<float> drawDistances = new List<float>();

                for(int index = 3; index < subsections.Length - 1; index++)
                    drawDistances.Add(float.Parse(subsections[index]));

                obj = new IdeTObj() {
                    ID = int.Parse(subsections[0]),
                    ModelName = subsections[1],
                    TextureName = subsections[2],
                    DrawDistances = drawDistances.ToArray(),
                    Flags = (IdeObjFlags)int.Parse(subsections[subsections.Length - 1]),
                    TimeOn = int.Parse(subsections[subsections.Length - 2]),
                    TimeOff = int.Parse(subsections[subsections.Length - 1])
                };
            }

            return true;
        }
        #endregion
    }
}
