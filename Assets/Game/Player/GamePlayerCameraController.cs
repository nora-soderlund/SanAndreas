using System;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class GamePlayerCameraController : MonoBehaviour {
        void Start() {
            this.gameObject.name = "Player Camera";
        }

        private float sensitivity = 3.0f;

        public float RotationX;
        public float RotationY;

        private float distance = 10.0f;

        void Update() {
            GameObject parent = gameObject.transform.parent.gameObject;

            if(parent.TryGetComponent<GamePlayerPedController>(out GamePlayerPedController pedController))
                pedController.UpdateCamera();

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            RotationY += mouseX * sensitivity;
            RotationX += mouseY * sensitivity;

            RotationX = Mathf.Clamp(RotationX, -40f, 40f);

            this.gameObject.transform.localEulerAngles = new Vector3(RotationX, RotationY, 0f);
            this.gameObject.transform.position = parent.transform.position - transform.forward * distance;
            
            //this.gameObject.transform.LookAt(parent.transform);
            //this.gameObject.transform.position = new Vector3(Mathf.Cos(horizontalAngle), 0, Mathf.Sin(horizontalAngle)) * distance;
        }
    }
}
