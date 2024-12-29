#nullable enable
using General;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace MyAssets.GameSystem.Scripts
{
    public class ComboCountPresenter:IInitializable
    {
        [Inject]
        public ComboCountPresenter(ComboTextViewMono comboTextViewMono, ComboCountHolder comboCountHolder,
            DisposeManager disposeManager)
        {
            comboCountHolder.ObserveEveryValueChanged(holder => holder.CurrentComboCount)
                .Subscribe(currentComboCount => comboTextViewMono.ChangeComboText(currentComboCount))
                .AddTo(disposeManager.CompositeDisposable);
        }

        //エントリーポイント用
        public void Initialize(){}
    }
}