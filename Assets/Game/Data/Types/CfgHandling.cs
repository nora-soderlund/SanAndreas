using System.Collections.Generic;

using UnityEngine;

namespace Game.Data.Types {
    public class CfgHandling {
        public string HandlingId;
        public float Mass;
        public float TurnMass;
        public float DragMult;
        public Vector3 CentreOfMass;
        public int PercentSubmerged;
        public float TractionMultiplier;
        public float TractionLoss;
        public float TractionBias;
        public int NumberOfGears;
        public float MaxVelocity;
        public float EngineAcceleration;
        public float EngineInertia;
        public char DriveType;
        public char EngineType;
        public float BrakeDeceleration;
        public float BrakeBias;
        public bool ABS;
        public float SteeringLock;

        public float SuspensionForceLevel;
        public float SuspensionDamingLevel;
        public float SuspensionHighSpeedComDamp;
        public float SuspensionUpperLimit;
        public float SuspensionLowerLimit;
        public float SuspensionBiasBetweenFrontAndRear;
        public float SuspensionAntiDiveMultiplier;

        public float SeatOffsetDistance;
        public float CollisionDamageMultiplier;
        public int MonetaryValue;

        // Add enumerator flags
        public string ModelFlags;
        public string HandlingFlags;

        public int FrontLights;
        public int RearLights;
        public int AnimationGroup;
    }
}
