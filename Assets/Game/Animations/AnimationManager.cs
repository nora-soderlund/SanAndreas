using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using Game.Data;
using Game.Data.Types;

namespace Game.Animations {
    class AnimationManager {
        private static readonly string path = "Assets/Data/Animations";

        public static List<Ifp> Animations = new List<Ifp>();

        public static void Initialize() {
            string[] files = Directory.GetFiles(path, "*.ifp");

            foreach(string file in files) {
                try {

                    //Debug.Log(String.Format("Library {0} loaded with {1}/{2} animations:", ifp.Name, ifp.NumberOfAnimations, ifp.Animations.Count));
                    //Debug.Log(String.Join(", ", ifp.Animations.Select(x => x.Name)));
                }
                catch(Exception exception) {
                    Debug.LogException(exception);
                }
            }

            Debug.Log($"Loaded {Animations.Count} animation libraries.");
        }
    
        public static Ifp GetLibrary(string library) {
            library = library.ToLower();

            Ifp ifp = Animations.Find(x => x.Name == library);
            
            if(ifp == null) {
                using IfpReader reader = new IfpReader(Path.Combine(path, library + ".ifp"));

                ifp = reader.Read();
                
                if(ifp == null)
                    return null;
            
                Animations.Add(ifp);
            }
        
            return ifp;
        }
    
        public static IfpAnimation GetIfpAnimation(string library, string animation) {
            library = library.ToLower();

            Ifp ifp = Animations.Find(x => x.Name == library);
            
            if(ifp == null) {
                using IfpReader reader = new IfpReader(Path.Combine(path, library + ".ifp"));

                ifp = reader.Read();
                
                if(ifp == null)
                    return null;
            
                Animations.Add(ifp);
            }

            animation = animation.ToLower();

            IfpAnimation ifpAnimation = ifp.Animations.Find(x => x.Name == animation);

            return ifpAnimation;
        }
    }
}
