using System;

namespace YodeGroup.Utility.Jobs
{
    [System.Serializable, Flags]
    public enum TimeToStart
    {
        None = 0,
        Awake = 1,
        Start = 2,
        OnEnable = 4,
        OnDisable = 8,
        OnApplicationFocus = 16,
        OnApplicationUnfocus = 32
    }
}