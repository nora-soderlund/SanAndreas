using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;

namespace Game.Data {
    public class IfpReader : IDisposable {
        private Stream stream;
        private BinaryReader reader;

        public IfpReader(string input) {
            this.stream = File.Open(input, FileMode.Open);
            this.reader = new BinaryReader(this.stream);
        }

        public Ifp Read() {
            string version = Encoding.Default.GetString(reader.ReadBytes(4));
            int offsetToEndOfFile = BitConverter.ToInt32(reader.ReadBytes(4), 0);
            string name = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(24)).ToLower();

            Ifp ifp = new Ifp() {
                Version = version,
                
                OffsetToEndOfFile = offsetToEndOfFile,
                
                Name = name.Substring(0, name.IndexOf('\0')),
                
                NumberOfAnimations = BitConverter.ToInt32(reader.ReadBytes(4), 0)
            };

            if(ifp.Version != "ANP3")
                throw new NotImplementedException("Ifp version " + ifp.Version + " is not implemented!");

            for(int index = 0; index < ifp.NumberOfAnimations; index++)
                ifp.Animations.Add(this.ReadAnimation());

            return ifp;
        }

        public IfpAnimation ReadAnimation() {
            string name = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(24)).ToLower();

            IfpAnimation ifpAnimation = new IfpAnimation() {
                Name = name.Substring(0, name.IndexOf('\0')).Trim(' '),

                NumberOfObjects = BitConverter.ToInt32(reader.ReadBytes(4), 0),

                SizeOfFrameData = BitConverter.ToInt32(reader.ReadBytes(4), 0),

                Validation = BitConverter.ToInt32(reader.ReadBytes(4), 0)
            };

            for(int index = 0; index < ifpAnimation.NumberOfObjects; index++)
                ifpAnimation.Objects.Add(this.readAnimationObject());

            return ifpAnimation;
        }

        private IfpAnimationObject readAnimationObject() {
            string name = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(24));

            IfpAnimationObject ifpAnimationObject = new IfpAnimationObject() {
                Name = name.Substring(0, name.IndexOf('\0')).Trim(' '),

                FrameType = BitConverter.ToInt32(reader.ReadBytes(4), 0),

                NumberOfFrames = BitConverter.ToInt32(reader.ReadBytes(4), 0),

                BoneId = BitConverter.ToInt32(reader.ReadBytes(4), 0)
            };

            for(int index = 0; index < ifpAnimationObject.NumberOfFrames; index++)
                ifpAnimationObject.Frames.Add(((ifpAnimationObject.FrameType == 4)?(this.readAnimationObjectRootFrame()):(this.readAnimationObjectChildFrame())));

            return ifpAnimationObject;
        }

        private IfpAnimationObjectFrame readAnimationObjectChildFrame() {
            // intentional, in unity Z is height and Y is depth...
            float x = (BitConverter.ToInt16(reader.ReadBytes(2), 0) / 4096f);
            float y = (BitConverter.ToInt16(reader.ReadBytes(2), 0) / 4096f);
            float z = (BitConverter.ToInt16(reader.ReadBytes(2), 0) / 4096f);

            float w = BitConverter.ToInt16(reader.ReadBytes(2), 0) / 4096f;

            return new IfpAnimationObjectFrame() {
                Quaternion = new Quaternion(x, y, z, w),

                TimeInSeconds = BitConverter.ToInt16(reader.ReadBytes(2), 0) / 60f
            };
        }

        private IfpAnimationObjectFrame readAnimationObjectRootFrame() {
            IfpAnimationObjectFrame ifpAnimationObjectFrame = this.readAnimationObjectChildFrame();

            // intentional, in unity Z is height and Y is depth...
            float x = (BitConverter.ToInt16(reader.ReadBytes(2), 0) / 1024f);
            float z = (BitConverter.ToInt16(reader.ReadBytes(2), 0) / 1024f);
            float y = (BitConverter.ToInt16(reader.ReadBytes(2), 0) / 1024f);;

            ifpAnimationObjectFrame.Translation = new Vector3(x, y, z);

            return ifpAnimationObjectFrame;
        }

        public void Dispose() {
            this.reader.Dispose();
            this.stream.Dispose();
        }
    }
}
