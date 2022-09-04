using System.Collections.Generic;

namespace Game.Data.Types {
    public class IfpAnimationObject {
        public string Name;

        public int FrameType;

        public int NumberOfFrames;

        public int BoneId;

        public List<IfpAnimationObjectFrame> Frames = new List<IfpAnimationObjectFrame>();
    }
}
