using Zenject;

namespace GameEnemies
{
    public class EnemySpawnerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            var v = Container.Bind<EnemySpawner>().FromComponentsInHierarchy().AsSingle().NonLazy();
        }
    }
}