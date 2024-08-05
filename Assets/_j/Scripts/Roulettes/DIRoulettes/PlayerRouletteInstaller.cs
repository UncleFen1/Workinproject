using Zenject;

namespace Roulettes
{
    public class PlayerRouletteInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerRoulette>().AsSingle().NonLazy();
        }
    }
}