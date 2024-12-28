#nullable enable
using UnityEngine;

namespace MyAssets.GameSystem.Scripts
{
    public class BackGroundViewMono:MonoBehaviour
    {
        [SerializeField] GameObject dayBackGround = null!;
        [SerializeField] GameObject morningBackGround = null!;
        [SerializeField] GameObject nightBackGround = null!;
        [SerializeField] GameObject zoneBackGround = null!;

        TimeState _currentTimeState;

        bool _isActiveZoneBackGround;
        
        public void ChangeBackGroundByTimeState(TimeState timeState)
        {
            _currentTimeState = timeState;
            
            if(_isActiveZoneBackGround)
                return;
            
            DisActiveAllBackGround();
            switch (timeState)
            {
                case TimeState.Day:
                    dayBackGround.SetActive(true);
                    break;
                case TimeState.Morning:
                    morningBackGround.SetActive(true);
                    break;
                case TimeState.Night:
                    nightBackGround.SetActive(true);
                    break;
            }
        }

        public void ChangeToZoneBackGround()
        {
            DisActiveAllBackGround();
            zoneBackGround.SetActive(true);
            _isActiveZoneBackGround = true;
        }
        
        public void RevertCurrentBackGround()
        {
            _isActiveZoneBackGround = false;
            ChangeBackGroundByTimeState(_currentTimeState);
        }

        void DisActiveAllBackGround()
        {
            dayBackGround.SetActive(false);
            morningBackGround.SetActive(false);
            nightBackGround.SetActive(false);
            zoneBackGround.SetActive(false);
        }
    }
}