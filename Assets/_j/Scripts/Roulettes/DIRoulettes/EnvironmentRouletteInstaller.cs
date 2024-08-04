using Zenject;

namespace Roulettes
{
    public class EnvironmentRouletteInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnvironmentRoulette>().AsSingle().NonLazy();
        }
    }
}