using Zenject;

namespace EventBus
{
    public class RegistratorInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}