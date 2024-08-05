using Zenject;

namespace GamePlayer
{
    public class PlayerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}