using System;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Animations;
using Game.Ped;
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

        void Start() {
            this.gameObject.name = "Player Controller";
            
            movementComponent = gameObject.GetComponent<GamePedMovementComponent>();
            cameraComponent = GameInstances.PlayerCamera.GetComponent<GamePlayerCameraController>();
        }

        void Update() {
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

        public void UpdateCamera() {
            if(Input.GetKey(KeyCode.W)) {
                transform.rotation = Quaternion.Euler(0, cameraComponent.transform.eulerAngles.y, 0);
                cameraComponent.RotationY = 0;
            }
        }
    }
}
