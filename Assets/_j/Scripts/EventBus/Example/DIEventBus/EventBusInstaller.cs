using Zenject;

namespace GameEventBus
{
    public class RegistratorInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}