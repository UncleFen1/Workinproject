using Zenject;

namespace Texts
{
public class TextsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITexts>().To<TextsExecutor>().AsSingle().NonLazy();
        }
    }
}