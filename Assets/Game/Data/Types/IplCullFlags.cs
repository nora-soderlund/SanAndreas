using System;

namespace Game.Data.Types {
    [Flags]
    public enum IplCullFlags
    {
        IfThirdPersonLockInOnClosestDistance = 1,
        CameraLockedOutsideOfZone = 2,
        Unknown = 4,
        RainAndHelicopterFree = 8,
        MilitaryMostWantedZone = 4096
    }
}
