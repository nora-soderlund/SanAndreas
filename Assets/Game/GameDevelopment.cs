using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace Game {
    class GameDevelopment : MonoBehaviour {
        public Dictionary<string, Dictionary<string, string>> Lines = new Dictionary<string, Dictionary<string, string>>();

        float framesDeltaTime = 0.0f;

        void Start() {
            Lines.Add("frames", new Dictionary<string, string>());
            Lines["frames"].Add("targetFrameRate", Application.targetFrameRate.ToString());
            Lines["frames"].Add("averageFrameRate", "");
            Lines["frames"].Add("currentFrameRate", "");
        }

        void Update() {
            framesDeltaTime += (Time.deltaTime - framesDeltaTime) * 0.1f;

            Lines["frames"]["currentFrameRate"] = Mathf.RoundToInt(1.0f / framesDeltaTime).ToString();
        }

        void OnGUI() {
            Lines["frames"]["averageFrameRate"] = Mathf.RoundToInt(Time.frameCount / Time.time).ToString();

            string label = "";

            foreach(KeyValuePair<string, Dictionary<string, string>> sections in Lines) {
                label += string.Join("\n", sections.Value.Select(x => x.Key + ": " + x.Value));
                
                label += "\n";
            }

            GUI.Label(new Rect(32f, 32f, Screen.width, Screen.height), label);
        }
    }
}
