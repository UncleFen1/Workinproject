using Zenject;

namespace GameEnemy
{
    public class EnemiesControllerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemiesController>().AsSingle().NonLazy();
        }
    }
}
