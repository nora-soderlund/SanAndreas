using System.Collections.Generic;

using UnityEngine;

namespace Game.Ped {
    public class GamePedBoneData {
        public GameObject GameObject;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    public class GamePedBonesComponent : MonoBehaviour {
        public void Start() {
            // cache all the bones... are they in a strict structure or are they dynamic??? investigate...
        }

        private Dictionary<string, GamePedBoneData> bones = new Dictionary<string, GamePedBoneData>();

        public GamePedBoneData GetBone(string name) {
            name = name.ToLower();

            if(!bones.ContainsKey(name)) {
                GamePedBoneData boneData = new GamePedBoneData() {
                    GameObject = GameObjectHelper.FindChild(this.gameObject, name)
                };

                if(boneData.GameObject != null) {
                    boneData.Position = boneData.GameObject.transform.localPosition;
                    boneData.Rotation = boneData.GameObject.transform.localRotation;
                }

                bones.Add(name, boneData);
            }

            return bones[name];
        }
    }
}