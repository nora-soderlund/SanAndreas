using System;

using UnityEngine;

using Game.Animations;
using Game.Data.Types;

namespace Game.Ped {
    enum GamePedAnimationState {
        Idling, StartWalking, Walking, Running, RunningStop
    }

    class GamePedMovementComponent : MonoBehaviour {
        private CharacterController controllerComponent;
        private GamePedAnimationComponent animationComponent;
        //private GamePlayerController playerController = null;

        private IfpAnimation startWalkingAnimation = AnimationManager.GetIfpAnimation("ped", "walk_start");
        private IfpAnimation walkingAnimation = AnimationManager.GetIfpAnimation("ped", "walk_gang1");
        private IfpAnimation runningAnimation = AnimationManager.GetIfpAnimation("ped", "run_gang1");
        private IfpAnimation runningStopAnimation = AnimationManager.GetIfpAnimation("ped", "run_stop");
        private IfpAnimation idlingAnimation = AnimationManager.GetIfpAnimation("ped", "idle_gang1");

        public GamePedAnimationState State;

        public Vector3 Velocity = Vector3.zero;
        public Vector3 AeroVelocity = Vector3.zero;
        public bool Running = false;

        private float walkSpeed = 3.0f;
        private float sprintSpeed = 6.0f;
        private float gravityValue = -9.81f;

        void Start() {
            if(!TryGetComponent<CharacterController>(out controllerComponent))
                controllerComponent = gameObject.AddComponent<CharacterController>();

            if(!TryGetComponent<GamePedAnimationComponent>(out animationComponent))
                throw new InvalidOperationException("GamePedAnimationComponent must be added before GamePedMovementComponent!");

            State = GamePedAnimationState.Idling;
            animationComponent.SetAnimation(idlingAnimation, true);
        }

        void Update() {
            if (controllerComponent.isGrounded && AeroVelocity.y < 0) {
                AeroVelocity.y = 0f;
            }

            if(Velocity != Vector3.zero) {
                if (Velocity.sqrMagnitude > 1)
                    Velocity.Normalize();

                controllerComponent.Move(Velocity * (Running?sprintSpeed:walkSpeed) * Time.deltaTime);
            }

            AeroVelocity.y += gravityValue * Time.deltaTime;
            controllerComponent.Move(AeroVelocity * Time.deltaTime);

            if(Velocity != Vector3.zero) {
                if(State != GamePedAnimationState.Running && Running) {
                    State = GamePedAnimationState.Running;
                    animationComponent.SetAnimation(runningAnimation, true);
                }
                else if(State != GamePedAnimationState.Walking && !Running) {
                    State = GamePedAnimationState.Walking;
                    animationComponent.SetAnimation(walkingAnimation, true);
                }
            }
            else if(State == GamePedAnimationState.Walking || State == GamePedAnimationState.Running) {
                State = GamePedAnimationState.Idling;
                animationComponent.SetAnimation(idlingAnimation, true);
            }

            Velocity = Vector3.zero;
        }
    }
}
