using System.Collections.Generic;

namespace Game.Data.Types {
    public class Ifp {
        public string Version;
        
        public int OffsetToEndOfFile;
        
        public string Name;
        
        public int NumberOfAnimations;

        public List<IfpAnimation> Animations = new List<IfpAnimation>();
    }
}
