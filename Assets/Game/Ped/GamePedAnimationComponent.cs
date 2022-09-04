using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;
using Game.Player;
using Game.Animations;

namespace Game.Ped {
    public class GamePedAnimationData {
        public IfpAnimationObject Object;
        public GamePedBoneData Bone;

        public int Frame = 0;
        public float Delta = 0;

        public Quaternion Start;
        public Quaternion End;
    }

    public class GamePedAnimationComponent : MonoBehaviour {
        public IfpAnimation IfpAnimation;
        private GamePedBonesComponent bonesComponent;

        public Vector3 Translation;

        public bool Infinite = true;

        public void Start() {
            this.bonesComponent = this.gameObject.GetComponent<GamePedBonesComponent>();
        }

        public void SetAnimation(IfpAnimation ifpAnimation, bool infinite) {
            if(this.IfpAnimation != null)
                this.Stop();

            this.IfpAnimation = ifpAnimation;
            this.Infinite = infinite;

            data = new List<GamePedAnimationData>();

            foreach(IfpAnimationObject ifpAnimationObject in this.IfpAnimation.Objects) {
                var animationData = new GamePedAnimationData() {
                    Object = ifpAnimationObject,
                    Bone = this.bonesComponent.GetBone(ifpAnimationObject.Name)
                };

                if(animationData.Bone.GameObject == null)
                    continue;

                animationData.Start = new Quaternion(
                    animationData.Bone.GameObject.transform.localRotation.x,
                    animationData.Bone.GameObject.transform.localRotation.y,
                    animationData.Bone.GameObject.transform.localRotation.z,
                    animationData.Bone.GameObject.transform.localRotation.w
                );

                animationData.End = Quaternion.Euler(
                    animationData.Object.Frames[animationData.Frame].Quaternion.eulerAngles.x,
                    360f - animationData.Object.Frames[animationData.Frame].Quaternion.eulerAngles.y,
                    360f - animationData.Object.Frames[animationData.Frame].Quaternion.eulerAngles.z
                );

                data.Add(animationData);
            }
        }

        public void Stop() {
            foreach(GamePedAnimationData data in this.data) {
                if(data.Bone.GameObject == null)
                    continue;

                data.Bone.GameObject.transform.localRotation = data.Bone.Rotation;
                data.Bone.GameObject.transform.localPosition = data.Bone.Position;
            }

            this.IfpAnimation = null;
            this.data = null;
        }

        private List<GamePedAnimationData> data;

        public void Update() {
            if(this.IfpAnimation == null)
                return;

            int index = 0;

            if(Translation != Vector3.zero)
                Translation = Vector3.zero;

            foreach(GamePedAnimationData data in this.data) {
                index++;

                if(data.Bone.GameObject == null)
                    continue;

                if(data.Frame == data.Object.Frames.Count)
                    continue;

                data.Delta += Time.deltaTime;

                if(data.Delta >= data.Object.Frames[data.Frame].TimeInSeconds) {
                    data.Frame++;

                    if(data.Object.Frames.Count == data.Frame) {
                        if(this.Infinite == true) {
                            data.Frame = 0;
                        
                            data.Delta = 0;
                        }
                        else continue;
                    }

                    data.Start = new Quaternion(
                        data.Bone.GameObject.transform.localRotation.x,
                        data.Bone.GameObject.transform.localRotation.y,
                        data.Bone.GameObject.transform.localRotation.z,
                        data.Bone.GameObject.transform.localRotation.w
                    );

                    data.End = Quaternion.Euler(
                        data.Object.Frames[data.Frame].Quaternion.eulerAngles.x,
                        360f - data.Object.Frames[data.Frame].Quaternion.eulerAngles.y,
                        360f - data.Object.Frames[data.Frame].Quaternion.eulerAngles.z
                    );

                    if(data.Object.Frames[data.Frame].Translation != Vector3.zero)
                        Translation = data.Object.Frames[data.Frame].Translation;
                }


                if(data.Object.Name != "Root") {
                    if(data.Object.Frames[data.Frame].TimeInSeconds == 0)
                        data.Bone.GameObject.transform.localRotation = data.End;
                    else
                        data.Bone.GameObject.transform.localRotation = Quaternion.Lerp(data.Start, data.End, data.Delta / data.Object.Frames[data.Frame].TimeInSeconds);    
                }
            }

            if(!Infinite) {
                foreach(GamePedAnimationData data in this.data) {
                    if(data.Frame < data.Object.Frames.Count)
                        return;
                }

                this.Stop();
            }
        }
    }
}
