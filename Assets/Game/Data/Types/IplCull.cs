using UnityEngine;

namespace Game.Data.Types {
    /// <summary>
    /// Used to place objects in the world.
    /// </summary>
    public class IplCull
    {
        /// <summary>
        /// A point which defines the center of the box in real world coordinates.
        /// </summary>
        public Vector3 Center;
        
        /// <summary>
        /// A point which corresponds to the lower left corner of the box.
        /// </summary>
        public Vector2 BottomLeft;
        
        /// <summary>
        /// A point which corresponds to the upper right corner of the box.
        /// </summary>
        public Vector2 TopRight; 
        
        /// <summary>
        /// The behaviour of the cull zone.
        /// </summary>
        public IplCullFlags Flags;
        
        /// <summary>
        /// Unknown behaviour, always 0.
        /// </summary>
        public int Unknown1;
        
        /// <summary>
        /// Unknown behaviour, always 0.
        /// </summary>
        public int Unknown2;
    }
}
