using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;
using Game.Animations;

namespace Game.Vehicles {
    // GameObject Hierachy
    // -> Vehicle
    //      -> chassis_dummy
    //          -> chassis
    //              -> bonnet_dummy
    //                  -> bonnet_[ok/dam]
    //                  -> ug_bonnet_[left/right](_dam)
    //        
    //              -> boot_dummy
    //                  -> boot_[ok/dam]
    //                  -> ug_spoiler(_dam)
    //    
    //              -> bump_[front/rear]_dummy
    //                  -> bump_front_[ok/dam]
    //    
    //              -> door_[l/r][f/b]_dummy
    //                  -> door_[l/r][f/b]_[ok/dam]
    //    
    //              -> windshield_dummy
    //                  -> windshield_[ok/dam]
    //    
    //              -> ug_nitro
    //          -> exhaust_[ok/dam]
    //      -> engine
    //      -> exhaust
    //      -> headlights
    //      -> headlights2
    //      -> ped_arm
    //      -> ped_frontseat
    //      -> petrolcap
    //      -> taillights
    //      -> taillights2
    //      -> wheel_[l/r][f/b]_dummy
    //          -> wheel 
    // -> Collisions
    // -> Players
    class GameVehicleComponent : MonoBehaviour {
        private Rigidbody rigidBody;

        private IdeCar ideCar;
        public CfgHandling CfgHandling;

        private GameVehicleBonesData bones;

        public List<GameVehicleWheelData> Wheels = new List<GameVehicleWheelData>();

        void Start() {
            bones = new GameVehicleBonesData(gameObject);

            ideCar = GameVehicleData.Cars.Find(x => x.ModelName == bones.Model.name);
            CfgHandling = GameVehicleData.Handling.Find(x => x.HandlingId == ideCar.HandlingId);

            foreach(GameObject damageModel in GameObjectHelper.GetChildren(bones.Model, x => x.EndsWith("_dam")))
                damageModel.SetActive(false);

            GameObject chassisVlo = GameObjectHelper.FindChild(bones.Model, "chassis_vlo", true);
            chassisVlo.SetActive(false);

            GameObject chassis = GameObjectHelper.FindChild(bones.Model, "chassis", true);

            rigidBody = gameObject.AddComponent<Rigidbody>();
            rigidBody.mass = CfgHandling.Mass;
            rigidBody.centerOfMass = CfgHandling.CentreOfMass;
            //rigidBody.drag = CfgHandling.DragMult;

            SetupWheels();
        }

        void FixedUpdate() {
            if(rigidBody.IsSleeping()) {
                rigidBody.AddForce(transform.up * 0.01f);
            }
        }

        void SetupWheels() {
            GameObject wheel = GameObjectHelper.FindChild(bones.Model, "wheel", true);
            wheel.SetActive(false);

            SetupWheel(wheel, bones.Dummies["wheel_lf_dummy"]);
            SetupWheel(wheel, bones.Dummies["wheel_rf_dummy"]);
            SetupWheel(wheel, bones.Dummies["wheel_lb_dummy"]);
            SetupWheel(wheel, bones.Dummies["wheel_rb_dummy"]);
        }

        void SetupWheel(GameObject model, GameVehicleDummyData dummy) {
            bool front = (dummy.Dummy.name[7] == 'f');
            float scale = (front)?(ideCar.WheelScale_Front):(ideCar.WheelScale_Rear);
            
            dummy.Normal = Instantiate(model, dummy.Dummy.transform);
            dummy.Normal.SetActive(true);

            if(model.transform.parent.name[6] == dummy.Dummy.name[6])
                dummy.Normal.transform.localRotation = Quaternion.Euler(dummy.Normal.transform.localRotation.eulerAngles.x, dummy.Normal.transform.localRotation.eulerAngles.y + 180f, dummy.Normal.transform.localRotation.eulerAngles.z);

            dummy.Normal.transform.localScale = new Vector3(scale, scale, scale);

            WheelCollider wheelCollider = dummy.Dummy.AddComponent<WheelCollider>();
            wheelCollider.radius = 0.25f * scale;
            //wheelCollider.wheelDampingRate = 1f;
            wheelCollider.wheelDampingRate = CfgHandling.CollisionDamageMultiplier;
            //wheelCollider.brakeTorque = CfgHandling.BrakeDeceleration;

            Wheels.Add(new GameVehicleWheelData() {
                GameObject = dummy.Dummy,
                WheelCollider = wheelCollider,
                Front = front
            });
        }

        bool IsWheelDummyInstantiated(GameObject dummy) {
            foreach(Transform child in dummy.transform) {
                if(child.name.Contains("wheel"))
                    return true;
            }

            return false;
        }
    }
}
