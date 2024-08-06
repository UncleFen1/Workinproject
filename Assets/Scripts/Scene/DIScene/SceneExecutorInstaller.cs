using Zenject;

namespace OldSceneNamespace
{
    public class SceneExecutorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneExecutor>().To<SceneExecutor>().AsSingle().NonLazy();
        }
    }
}

