using System;
using System.Linq;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Animations;
using Game.Ped;
using Game.Vehicles;
using Game.Data.Types;

namespace Game.Player {
    public class GamePlayerPedController : MonoBehaviour {
        private GamePedMovementComponent movementComponent;
        private GamePlayerCameraController cameraComponent;

        private float walkSpeed = 3.0f;
        private float sprintSpeed = 6.0f;

        private float gravityValue = -9.81f;
        private bool groundedPlayer;
        private float jumpHeight = 1.0f;
        public bool Sprinting = false;

        public void Start() {
            this.gameObject.name = "Player Controller";
            
            movementComponent = gameObject.GetComponent<GamePedMovementComponent>();
            cameraComponent = GameInstances.PlayerCamera.GetComponent<GamePlayerCameraController>();
        }

        public void Update() {
            processMovementUpdate();
            processActionsUpdate();
        }

        public void UpdateCamera() {
            if(Input.GetKey(KeyCode.W)) {
                transform.rotation = Quaternion.Euler(0, cameraComponent.transform.eulerAngles.y, 0);
                cameraComponent.RotationY = 0;
            }
        }

        private void processMovementUpdate() {
            bool walking = Input.GetKey(KeyCode.W);

            Sprinting = Input.GetKey(KeyCode.LeftShift);

            float speed = (Sprinting)?(sprintSpeed):(walkSpeed);

            if(walking)
                movementComponent.Velocity += movementComponent.transform.forward * speed;
            else if(Input.GetKey(KeyCode.S))
                movementComponent.Velocity -= movementComponent.transform.forward * speed;
            
            if(Input.GetKey(KeyCode.A))
                movementComponent.Velocity -= movementComponent.transform.right * speed;
            else if(Input.GetKey(KeyCode.D))
                movementComponent.Velocity += movementComponent.transform.right * speed;
 
            if (Input.GetButtonDown("Jump") && groundedPlayer) {
                movementComponent.Velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }

        private void processActionsUpdate() {
            if(!Input.GetKeyDown(KeyCode.F))
                return;

            GameObject vehicle = GameInstances.Vehicles.Where(x => Vector3.Distance(x.transform.position, transform.position) < 5.0f).OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();

            if(vehicle == null)
                return;

            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.GetComponent<GamePedAnimationComponent>().SetAnimation(AnimationManager.GetIfpAnimation("car", "tap_hand"), true);

            GameVehicleComponent vehicleComponent = vehicle.GetComponent<GameVehicleComponent>();

            transform.SetParent(vehicle.transform);
            transform.position = Vector3.zero;
            
            if(vehicleComponent.Bones.Points.TryGetValue("ped_frontseat", out GameObject point)) {
                transform.localPosition = point.transform.position - vehicle.transform.position;
                transform.localRotation = point.transform.localRotation;
            }

            GameInstances.PlayerCamera.transform.SetParent(vehicle.transform);

            GameObject.Destroy(gameObject.GetComponent<GamePlayerPedController>());
            vehicle.AddComponent<GamePlayerVehicleController>();
        }
    }
}
