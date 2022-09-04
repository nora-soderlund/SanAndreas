using System.Collections.Generic;

namespace Game.Data.Types {
    public class IfpAnimation {
        public string Name;

        public int NumberOfObjects;

        public int SizeOfFrameData;

        public int Validation;

        public List<IfpAnimationObject> Objects = new List<IfpAnimationObject>();
    }
}
