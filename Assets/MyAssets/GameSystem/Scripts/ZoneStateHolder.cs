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
            CurrentZoneState = ZoneState.InZone;
        }

        public void EndZoneMode()
        {
            CurrentZoneState = ZoneState.NotZone;
        }
        
        public bool IsZoneState()
        {
            return CurrentZoneState == ZoneState.InZone;
        }
    }
}