#nullable enable
using Cysharp.Threading.Tasks;
using General;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class TimeStatePresenter:IInitializable
    {
        [Inject]
        public TimeStatePresenter(TimeStateHolder timeStateHolder,
            BackGroundViewMono backGroundViewMono,DisposeManager disposeManager)
        {
            timeStateHolder.ObserveEveryValueChanged(holder => holder.CurrentTimeState)
                .Subscribe(currentTimeState =>
                {
                    backGroundViewMono.ChangeBackGroundByTimeState(currentTimeState);
                })
                .AddTo(disposeManager.CompositeDisposable);
        }


        //エントリーポイント用
        public void Initialize(){}
    }
}