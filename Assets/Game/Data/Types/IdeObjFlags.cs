using System;

namespace Game.Data.Types {
    /*
        Binary	Decimal	Description
        
        0000000000000000000000000000001	1	Wet Effect
        0000000000000000000000000000010	2	Time Object Night Flag
        0000000000000000000000000000100	4	ALPHA Transparency 1
        0000000000000000000000000001000	8	ALPHA Transparency 2 *
        0000000000000000000000000010000	16	Time Object Day Flag
        0000000000000000000000000100000	32	Interior-Object **
        0000000000000000000000001000000	64	Disable Shadow Mesh
        0000000000000000000000010000000	128	Excludes object from culling
        0000000000000000000000100000000	256	Disable Draw Distance for LODs
        0000000000000000000001000000000	512	Breakable Glass **
        0000000000000000000010000000000	1024	Breakable Glass with crack **
        0000000000000000000100000000000	2048	Garage door **
        0000000000000000001000000000000	4096	2-Clump-Object ** (Switches from Clump 2 to 1 after Collision)
        0000000000000000010000000000000	8192	Small Vegetation. Object sways in strong wind (?)
        0000000000000000100000000000000	16384	Standard Vegetation ** (Palms in Hotels, etc.) (?)
        0000000000000001000000000000000	32768	Use timecycle PoleShadow flag
        0000000000000010000000000000000	65536	Explosive-Flag **
        0000000000000100000000000000000	131072	UNKNOWN (Seems to be an SCM Flag) (?)
        0000000000001000000000000000000	262144	UNKNOWN (1 Object in Jizzy`s Club) (?)
        0000000000010000000000000000000	524288	UNKNOWN (?)
        0000000000100000000000000000000	1048576	Graffiti Flag
        0000000001000000000000000000000	2097152	Disable backface culling
        0000000010000000000000000000000	4194304	UNKNOWN (Parts of a statue in Atrium) (?)
        ...		All flags in this range are unused/unknown
        1000000000000000000000000000000	1073741824	Unknown
    */
    
    [Flags]
    public enum IdeObjFlags
    {
        IfThirdPersonLockInOnClosestDistance = 1,
        CameraLockedOutsideOfZone = 2,
        Unknown = 4,
        RainAndHelicopterFree = 8,
        MilitaryMostWantedZone = 4096
    }
}
