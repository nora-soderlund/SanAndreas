using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace Game {
    class GameObjectHelper {
        public static bool TryFindChild(GameObject root, Predicate<string> predicate, out GameObject result) {
            result = FindChild(root, predicate);

            if(result == null)
                return false;

            return true;
        }

        public static GameObject FindChild(GameObject root, Predicate<string> predicate) {
            foreach(Transform child in root.transform) {
                string name = child.name.ToLower();
                int index = name.IndexOf('.');

                if(index != -1)
                    name = name.Substring(0, index);

                if(predicate(name))
                    return child.gameObject;
            }

            foreach(Transform child in root.transform) {
                if(TryFindChild(child.gameObject, predicate, out GameObject result))
                    return result;
            }

            return null;
        }

        public static bool TryGetChild(GameObject root, Predicate<string> predicate, out GameObject result) {
            result = GetChild(root, predicate);

            if(result == null)
                return false;

            return true;
        }

        public static GameObject GetChild(GameObject root, Predicate<string> predicate) {
            foreach(Transform child in root.transform) {
                string name = child.name.ToLower();
                int index = name.IndexOf('.');

                if(index != -1)
                    name = name.Substring(0, index);

                if(predicate(name))
                    return child.gameObject;
            }

            return null;
        }

        public static List<GameObject> GetChildren(GameObject root, Predicate<string> predicate) {
            List<GameObject> results = new List<GameObject>();

            foreach(Transform child in root.transform) {
                string name = child.name.ToLower();
                int index = name.IndexOf('.');

                if(index != -1)
                    name = name.Substring(0, index);

                if(predicate(name))
                    results.Add(child.gameObject);
            }

            foreach(Transform child in root.transform) {
                results.AddRange(GetChildren(child.gameObject, predicate));
            }

            return results;
        }

        //////////////////////////////
        public static bool TryFindChildEndingWith(GameObject gameObject, string id, out GameObject result, bool removeAfterDot = false) {
            result = FindChildEndingWith(gameObject, id, removeAfterDot);

            if(result == null)
                return false;

            return true;
        }

        public static bool TryFindChild(GameObject gameObject, string id, out GameObject result, bool removeAfterDot = false) {
            result = FindChild(gameObject, id, removeAfterDot);

            if(result == null)
                return false;

            return true;
        }

        public static GameObject FindChildEndingWith(GameObject gameObject, string id, bool removeAfterDot = false) {
            id = id.ToLower();

            foreach(Transform child in gameObject.transform) {
                string name = child.name.ToLower().Trim(' ');

                if(removeAfterDot) {
                    if(name.IndexOf('.') != -1)
                        name = name.Substring(0, name.IndexOf('.'));
                }

                if(name.EndsWith(id))
                    return child.gameObject;
            }

            foreach(Transform child in gameObject.transform) {
                GameObject result = FindChildEndingWith(child.gameObject, id, removeAfterDot);

                if(result != null)
                    return result.gameObject;
            }

            return null;
        }

        public static GameObject FindChild(GameObject gameObject, string id, bool removeAfterDot = false) {
            id = id.ToLower();

            foreach(Transform child in gameObject.transform) {
                string name = child.name.ToLower().Trim(' ');

                if(removeAfterDot) {
                    if(name.IndexOf('.') != -1)
                        name = name.Substring(0, name.IndexOf('.'));
                }

                if(name == id)
                    return child.gameObject;
            }

            foreach(Transform child in gameObject.transform) {
                GameObject result = FindChild(child.gameObject, id, removeAfterDot);

                if(result != null)
                    return result.gameObject;
            }

            return null;
        }

        public static List<GameObject> FindAllChildren(GameObject gameObject, string id) {
            List<GameObject> result = new List<GameObject>();

            id = id.ToLower();

            foreach(Transform child in gameObject.transform) {
                string name = child.name.ToLower().Trim(' ');

                if(name.Contains(id))
                    result.Add(child.gameObject);
            }

            foreach(Transform child in gameObject.transform) {
                List<GameObject> results = FindAllChildren(child.gameObject, id);

                if(results != null)
                    result.AddRange(results);
            }

            return result.Count > 0 ? result : null;
        }

        public static List<GameObject> FindAllChildrenEndingWith(GameObject gameObject, string id, bool removeAfterDot = false) {
            List<GameObject> result = new List<GameObject>();

            id = id.ToLower();

            foreach(Transform child in gameObject.transform) {
                string name = child.name.ToLower().Trim(' ');

                if(removeAfterDot && name.IndexOf('.') != -1)
                    name = name.Substring(0, name.IndexOf('.'));

                if(name.EndsWith(id))
                    result.Add(child.gameObject);
            }

            foreach(Transform child in gameObject.transform) {
                List<GameObject> results = FindAllChildrenEndingWith(child.gameObject, id, removeAfterDot);

                if(results != null)
                    result.AddRange(results);
            }

            return result.Count > 0 ? result : null;
        }
    }
}
