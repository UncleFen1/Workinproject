using Zenject;

namespace Registrator
{
public class RegistratorInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRegistrator>().To<RegistratorExecutor>().AsSingle().NonLazy();
        }
    }
}