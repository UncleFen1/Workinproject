using Zenject;

namespace Roulettes
{
    public class EnemyRouletteInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyRoulette>().AsSingle().NonLazy();
        }
    }
}