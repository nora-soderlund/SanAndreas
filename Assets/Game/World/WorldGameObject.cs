using UnityEngine;

namespace Game.World {
    class WorldGameObject {
        public int ModelID;

        public Vector3 Position;
        public Quaternion Rotation;

        public float Distance;

        public GameObject GameObject;

        public bool Active {
            get {
                return (GameObject == null)?(false):(GameObject.activeSelf);
            }
        }
    }
}
