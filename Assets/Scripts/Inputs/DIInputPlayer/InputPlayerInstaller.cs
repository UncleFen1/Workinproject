using Zenject;

namespace Inputs
{
    public class InputPlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputPlayerExecutor>().To<InputPlayerExecutor>().AsSingle().NonLazy();
        }
    }
}

