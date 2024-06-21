using Zenject;

namespace UI
{
    public class UIGameExecutorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIGameExecutor>().To<UIGameExecutor>().AsSingle().NonLazy();
        }
    }
}

