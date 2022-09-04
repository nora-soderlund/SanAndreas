using System.Collections.Generic;

using UnityEngine;

namespace Game.Vehicles {
    class GameVehicleBonesData {
        public GameObject Model;
        public GameObject Collisions;
        public GameObject Players;

        public Dictionary<string, GameVehicleDummyData> Dummies = new Dictionary<string, GameVehicleDummyData>();
        
        public GameVehicleBonesData(GameObject gameObject) {
            Model = gameObject.transform.GetChild(0).gameObject;

            // collisions

            Collisions = new GameObject("Collisions");
            Collisions.transform.SetParent(gameObject.transform);
            Collisions.transform.localPosition = Vector3.zero;
            
            // i really should group these...
            Model.transform.rotation = Quaternion.Euler(Model.transform.rotation.eulerAngles.x, Model.transform.rotation.eulerAngles.y, 180);
            Collisions.transform.rotation = Quaternion.Euler(0, 0, 180);

            List<Transform> children = new List<Transform>();

            foreach(Transform child in gameObject.transform) {
                if(child.gameObject.name.Contains("col")) {
                    children.Add(child);

                    if(child.gameObject.name.Contains("ShadowMesh"))
                        child.gameObject.SetActive(false);
                    else if(child.gameObject.name.Contains("Sphere"))
                        child.gameObject.AddComponent<SphereCollider>();
                    else if(child.gameObject.name.Contains("ColMesh"))
                        child.gameObject.SetActive(false);
                }
            }

            foreach(Transform child in children)
                child.SetParent(Collisions.transform);

            // dummies

            List<GameObject> dummies = GameObjectHelper.GetChildren(Model, x => x.EndsWith("_dummy"));

            foreach(GameObject dummy in dummies) {
                string name = dummy.name.ToLower();
                int index = name.IndexOf('.');

                if(index != -1)
                    name = name.Substring(0, index);

                dummy.name = name;

                Dummies.Add(name, new GameVehicleDummyData(dummy));
            }
        }
    }

    class GameVehicleDummyData {
        public GameObject Dummy;
        public GameObject Normal;
        public GameObject Damaged;

        public GameVehicleDummyData(GameObject dummy) {
            Dummy = dummy;

            if(GameObjectHelper.TryGetChild(dummy, x => x == dummy.name.Replace("_dummy", "_ok"), out GameObject model))
                Normal = model;
            else if(GameObjectHelper.TryGetChild(dummy, x => x == dummy.name.Replace("_dummy", ""), out model))
                Normal = model;

            if(GameObjectHelper.TryGetChild(dummy, x => x == dummy.name.Replace("_dummy", "_da,"), out model))
                Damaged = model;
        }
    }
}
