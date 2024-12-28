#nullable enable
namespace MyAssets.GameSystem.Scripts
{
    public class TimeStateHolder
    {
        public TimeState CurrentTimeState { get; set; }

        public void ChangeTimeState(TimeState timeState)
        {
            CurrentTimeState = timeState;
        }
        
    }
}