using UnityEngine;

namespace Game.Data.Types {
    /// <summary>
    /// Used to place objects in the world.
    /// </summary>
    public class IplInst
    {
        /// <summary>
        /// A number which is used to identify the object as defined in the IDE.
        /// </summary>
        public int ID;
        
        /// <summary>
        /// The name of the model inside an image file which is defined in the gta.dat file without extension.
        /// </summary>
        public string ModelName;
        
        /// <summary>
        /// A number defining the interior (render-level) the object is located in.
        /// </summary>
        public int Interior; 
        
        /// <summary>
        /// The position of the object as floating point values in the world.
        /// </summary>
        public Vector3 Position;
        
        /// <summary>
        /// The rotation of the object as quaternion.
        /// </summary>
        public Quaternion Rotation;
        
        /// <summary>
        /// The number of the LOD which is located inside the same instance block as the current model.
        /// By default this is -1 which means no LOD is defined.
        /// </summary>
        public int LOD;
    }
}
