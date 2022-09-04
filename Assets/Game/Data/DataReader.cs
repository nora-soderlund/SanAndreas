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
    public class DataReader : IDisposable
    {
        #region Internal properties
        internal StreamReader reader;

        internal string section;
        #endregion

        #region Constructor
        public DataReader(string file) {
            this.reader = new StreamReader(file);
        }
        #endregion

        #region Internal methods
        internal bool readSection(string section) {
            this.resetReader();

            while(!reader.EndOfStream) {
                string line = reader.ReadLine();

                if(line.ToLower() == section) {
                    this.section = section;

                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Private methods
        private void resetReader() {
            this.reader.DiscardBufferedData();
            this.reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
        #endregion

        #region Public methods
        public void Dispose() {
            reader.Dispose();
        }
        #endregion
    }
}
