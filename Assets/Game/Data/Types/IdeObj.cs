using System.Collections.Generic;

namespace Game.Data.Types {
    /// <summary>
    /// Used to place objects in the world.
    /// </summary>
    public class IdeObj
    {
        /// <summary>
        /// A number which is used to identify the objects as defined in the IDE.
        /// </summary>
        public int ID;
        
        /// <summary>
        /// The name of the model without extension.
        /// </summary>
        public string ModelName;
        
        /// <summary>
        /// The name of the texture dictionary without extension.
        /// </summary>
        public string TextureName;
        
        /// <summary>
        /// The amount of sub objects, e.g. damaged parts, usually 1 (optional, default: 1)
        /// </summary>
        public int ObjectCount = 1;
        
        /// <summary>
        /// Draw distance in units, one for each sub object
        /// </summary>
        public float[] DrawDistances;
        
        /// <summary>
        /// Object flags, defining special behavior, default 0
        /// </summary>
        public IdeObjFlags Flags;

        public List<string> Origin = new List<string>();
    }
}
