#nullable enable
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class TimeAndScorePresenter:IInitializable
    {
        [Inject]
        public TimeAndScorePresenter(GameTimeHolder gameTimeHolder, GameScoreHolder gameScoreHolder,
            TimeTextViewMono timeTextViewMono,ScoreTextViewMono scoreTextViewMono)
        {
            gameTimeHolder.ObserveEveryValueChanged(holder => holder.CurrentTime)
                .Subscribe(currentTime => timeTextViewMono.
                    ChangeTimeText(gameTimeHolder.GameLimitTime-Mathf.CeilToInt(currentTime)));
            
            gameScoreHolder.ObserveEveryValueChanged(holder => holder.GameScore)
                .Subscribe(score => scoreTextViewMono.ChangeScoreText(score));
        }

        //エントリーポイント用
        public void Initialize(){}
    }
}