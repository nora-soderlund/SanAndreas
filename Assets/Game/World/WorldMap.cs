using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Data;
using Game.Data.Types;

using Game.World.Types;
using Game.Player;

namespace Game.World {
    public class WorldMap : MonoBehaviour {
        private static List<WorldGameObject> StaticGameObjects = new List<WorldGameObject>();

        public static void Initialize() {
            WorldMapData.Start();

            Debug.Log("Instantinating all static objects");

            foreach(IplInst iplInst in WorldMapData.StaticObjects) {
                IdeObj ideObj = WorldMapData.ObjectDefinitions.Find(x => x.ID == iplInst.ID);

                if(ideObj == null)
                    continue;

                if(!WorldMapData.GetGameObject(ideObj, out GameObject gameObject))
                    continue;

                WorldGameObject worldGameObject = new WorldGameObject() {
                    ModelID = ideObj.ID,
                    Position = iplInst.Position,
                    Rotation = iplInst.Rotation,
                };

                if(ideObj.DrawDistances.FirstOrDefault() != 0) {
                    worldGameObject.Distance = ideObj.DrawDistances.FirstOrDefault() * 2f;
                }

                StaticGameObjects.Add(worldGameObject);
            }
        }

        public static void Update() {
            Vector3 position = Camera.main.transform.position;

            foreach(WorldGameObject gameObject in StaticGameObjects) {
                float distance = Vector3.Distance(gameObject.Position, position);

                bool active = distance < gameObject.Distance;
                
                if(active != gameObject.Active) {
                    if(gameObject.GameObject == null) {
                        if(WorldMapData.GetGameObject(gameObject.ModelID, out GameObject gameObjectModel)) {
                            gameObject.GameObject = Instantiate(gameObjectModel, gameObject.Position, gameObject.Rotation);

                            gameObject.GameObject.AddComponent<MeshCollider>();
                        }
                    }
                    else
                        gameObject.GameObject.SetActive(active);
                }
            }
        }
    }
}
