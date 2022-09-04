using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Data;
using Game.Data.Types;

namespace Game.World {
    public class WorldMapData
    {
        public static List<IdeObj> ObjectDefinitions = new List<IdeObj>();

        public static List<IplInst> StaticObjects = new List<IplInst>();

        public static Dictionary<int, GameObject> GameObjects = new Dictionary<int, GameObject>();

        public static bool GetGameObject(int id, out GameObject gameObject) {
            IdeObj ideObj = ObjectDefinitions.Find(x => x.ID == id);

            if(ideObj == null) {
                gameObject = null;
                
                return false;
            }

            return GetGameObject(ideObj, out gameObject);
        }

        public static bool GetGameObject(IdeObj ideObj, out GameObject gameObject) {
            if(GameObjects.ContainsKey(ideObj.ID)) {
                gameObject = GameObjects[ideObj.ID];
            
                return gameObject != null;
            }

            gameObject = null;

            string modelName = ideObj.ModelName;

            if(!File.Exists(Path.Combine("Assets/Resources/gta3", modelName + ".fbx")))
                return false;

            gameObject = Resources.Load<GameObject>("gta3/" + modelName);

            GameObjects.Add(ideObj.ID, gameObject);

            // we don't return this before the dictionary addition because Resources.Load is an
            // expensive operation to run several times if the resource just doesn't exist
            return (gameObject != null);
        }

        public static bool GetGameObject(string modelName, out GameObject gameObject) {
            gameObject = null;

            if(!File.Exists(Path.Combine("Assets/Resources/gta3", modelName + ".fbx")))
                return false;

            gameObject = Resources.Load<GameObject>("gta3/" + modelName);

            return (gameObject != null);
        }

        public static void Start()
        {
            Debug.Log("Loading gta.dat");

            using(StreamReader reader = new StreamReader("Assets/Data/gta.dat")) {
                while(!reader.EndOfStream) {
                    string line = reader.ReadLine();

                    if(line.Length == 0 || line[0] == '#')
                        continue;

                    string[] sections = line.Split(' ');

                    switch(sections[0]) {
                        case "IDE": {
                            using IdeReader ideReader = new IdeReader(Path.Combine("Assets", sections[1]));

                            while(ideReader.ReadObj(out IdeObj ideObj)) {
                                if(!ObjectDefinitions.Any(x => x.ID == ideObj.ID))
                                    ObjectDefinitions.Add(ideObj);

                                // this is only for debug intentions
                                ObjectDefinitions.Find(x => x.ID == ideObj.ID).Origin.Add(sections[1]);
                            }

                            break;
                        }

                        case "IPL": {
                            using IplReader iplReader = new IplReader(Path.Combine("Assets", sections[1]));

                            while(iplReader.ReadInst(out IplInst iplInst)) {
                                if(iplInst.ModelName.ToLower().StartsWith("lod"))
                                    continue;

                                StaticObjects.Add(iplInst);
                            }

                            break;
                        }

                        default: {

                            break;
                        }
                    }
                }
            }

            Debug.Log("Reading all streamed item placements");

            string[] files = Directory.GetFiles("Assets/Data/maps/gta3/");

            foreach(string file in files) {
                using IplReader iplReader = new IplReader(Path.Combine("Assets/Data/maps/gta3", Path.GetFileName(file)));

                while(iplReader.ReadInst(out IplInst iplInst)) {
                    if(iplInst.ModelName.ToLower().StartsWith("lod"))
                        continue;

                    StaticObjects.Add(iplInst);
                }
            }

            /*Debug.Log("Preloading all item definitions");

            foreach(IdeObj ideObj in ObjectDefinitions) {
                if(!File.Exists(Path.Combine("Assets/Resources/gta3", ideObj.ModelName + ".fbx")))
                    continue;

                GameObjects.Add(ideObj.ID, Resources.Load<GameObject>("gta3/" + ideObj.ModelName));
            }*/
        }
    }
}
