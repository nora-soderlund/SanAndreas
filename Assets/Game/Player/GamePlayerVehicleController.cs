using System;
using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;
using Game.Animations;

namespace Game.Vehicles {
    class GamePlayerVehicleController : MonoBehaviour {
        private GameVehicleComponent vehicleComponent;

        void Start() {
            if(!gameObject.TryGetComponent<GameVehicleComponent>(out vehicleComponent))
                throw new InvalidOperationException("GameVehicleComponent must be added before GamePlayerVehicleController!");
        }

        public void FixedUpdate()
        {
            float motor = Input.GetAxis("Vertical");
            float steering = Input.GetAxis("Horizontal");
            bool braking = Input.GetKey(KeyCode.Space);

            foreach (GameVehicleWheelData wheelData in vehicleComponent.Wheels) {
                if (wheelData.Front) {
                    wheelData.WheelCollider.steerAngle = steering * vehicleComponent.CfgHandling.SteeringLock;
                }
                
                if(!braking) {
                    if((vehicleComponent.CfgHandling.DriveType == 'F' && wheelData.Front) || (vehicleComponent.CfgHandling.DriveType == 'R' && !wheelData.Front) || vehicleComponent.CfgHandling.DriveType == '4')
                        wheelData.WheelCollider.motorTorque = motor * (vehicleComponent.CfgHandling.EngineAcceleration * 1000);

                    wheelData.WheelCollider.brakeTorque = 0;
                }
                else {
                    wheelData.WheelCollider.motorTorque = 0;
                    wheelData.WheelCollider.brakeTorque = vehicleComponent.CfgHandling.BrakeDeceleration * 1000;
                }

                ApplyLocalPositionToVisuals(wheelData);
            }
        }

        public void ApplyLocalPositionToVisuals(GameVehicleWheelData wheelData) {        
            wheelData.WheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        
            //wheelData.GameObject.transform.position = position;
            wheelData.GameObject.transform.rotation = rotation;
        }
    }
}
