#nullable enable
using UnityEngine;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public enum ZoneState
    {
        NotZone,
        InZone
    }
    
    public class ZoneStateHolder
    {
        public ZoneState CurrentZoneState { get; set; }

        public void GoZoneMode()
        {
            Debug.LogError("InZone");
            CurrentZoneState = ZoneState.InZone;
        }
        
        public bool IsZoneState()
        {
            return CurrentZoneState == ZoneState.InZone;
        }
    }
}