#nullable enable
using Cysharp.Threading.Tasks;
using General;
using Model.Player;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace MyAssets.Player.Scripts
{
    public class HiddenObjectPresenter : IInitializable
    {
        HiddenObjectStateHolder _hiddenObjectStateHolder;
        HiddenObjectViewMono _hiddenObjectViewMono;
        DisposeManager _disposeManager;
        
        [Inject]
        public HiddenObjectPresenter(HiddenObjectStateHolder hiddenObjectStateHolder,
            HiddenObjectViewMono hiddenObjectViewMono,DisposeManager disposeManager)
        {
            _hiddenObjectStateHolder = hiddenObjectStateHolder;
            _hiddenObjectViewMono = hiddenObjectViewMono;
            _disposeManager = disposeManager;

            SubscribeWithHiddenObject();
        }
        
        // エントリーポイント用
        public void Initialize(){}
        
        void SubscribeWithHiddenObject()
        {
            _hiddenObjectStateHolder
                .ObserveEveryValueChanged(stateHolder => stateHolder.CurrentHiddenObject)
                .Subscribe(hiddenObject => _hiddenObjectViewMono.ChangeHiddenObjectPosition(hiddenObject))
                .AddTo(_disposeManager.CompositeDisposable);
        }
    }
}