using Zenject;

namespace GameSceneManagement
{
    public class GameSceneInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSceneController>().AsSingle().NonLazy();
        }
    }
}