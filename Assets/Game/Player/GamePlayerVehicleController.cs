using System;
using System.Collections.Generic;

using UnityEngine;

using Game.Data.Types;
using Game.Animations;
using Game.Ped;
using Game.Player;

namespace Game.Vehicles {
    class GamePlayerVehicleController : MonoBehaviour {
        private GameVehicleComponent vehicleComponent;

        void Start() {
            if(!gameObject.TryGetComponent<GameVehicleComponent>(out vehicleComponent))
                throw new InvalidOperationException("GameVehicleComponent must be added before GamePlayerVehicleController!");
        }

        public void Update() {
            processActionsUpdate();
        }

        public void FixedUpdate()
        {
            float motor = Input.GetAxis("Vertical");
            float steering = Input.GetAxis("Horizontal");
            float braking = (Input.GetKey(KeyCode.Space) == true)?(1f):((motor == 0.0f)?(0.1f):(0.0f));

            if(steering != 0)
                vehicleComponent.RigidBody.mass = vehicleComponent.CfgHandling.TurnMass;
            else
                vehicleComponent.RigidBody.mass = vehicleComponent.CfgHandling.Mass;

            foreach (GameVehicleWheelData wheelData in vehicleComponent.Wheels) {
                if (wheelData.Front) {
                    wheelData.WheelCollider.steerAngle = steering * vehicleComponent.CfgHandling.SteeringLock;
                }
                
                if((vehicleComponent.CfgHandling.DriveType == 'F' && wheelData.Front) || (vehicleComponent.CfgHandling.DriveType == 'R' && !wheelData.Front) || vehicleComponent.CfgHandling.DriveType == '4') {
                    if(motor != 0f)
                        wheelData.WheelCollider.motorTorque = motor * (vehicleComponent.CfgHandling.Mass * vehicleComponent.CfgHandling.EngineAcceleration);
                }
                else
                    wheelData.WheelCollider.motorTorque = 0;

                float torque = (vehicleComponent.CfgHandling.Mass * vehicleComponent.CfgHandling.EngineAcceleration * vehicleComponent.CfgHandling.BrakeDeceleration) * 2;

                if(wheelData.Front)
                    wheelData.WheelCollider.brakeTorque = braking * (torque * vehicleComponent.CfgHandling.BrakeBias);
                else
                    wheelData.WheelCollider.brakeTorque = braking * (torque * (1f - vehicleComponent.CfgHandling.BrakeBias));

                ApplyLocalPositionToVisuals(wheelData);
            }
        }

        public void ApplyLocalPositionToVisuals(GameVehicleWheelData wheelData) {        
            wheelData.WheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        
            //wheelData.GameObject.transform.position = position;
            wheelData.GameObject.transform.rotation = rotation;
        }

        private void processActionsUpdate() {
            if(!Input.GetKeyDown(KeyCode.F))
                return;
            
            foreach (GameVehicleWheelData wheelData in vehicleComponent.Wheels) {
                wheelData.WheelCollider.motorTorque = 0;
                wheelData.WheelCollider.brakeTorque = 0;
            }

            GameInstances.PlayerPed.transform.SetParent(transform.parent);
            GameInstances.PlayerCamera.transform.SetParent(GameInstances.PlayerPed.transform);
            
            GameInstances.PlayerPed.GetComponent<CharacterController>().enabled = true;
            GameInstances.PlayerPed.GetComponent<GamePedAnimationComponent>().Stop();

            GameObject.Destroy(gameObject.GetComponent<GamePlayerVehicleController>());
            GameInstances.PlayerPed.AddComponent<GamePlayerPedController>();
        }
    }
}
