using Zenject;

namespace UI
{
    public class MenuExecutorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMenuExecutor>().To<MenuExecutor>().AsSingle().NonLazy();
        }
    }
}

