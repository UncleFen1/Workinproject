using Zenject;

namespace GameGrid
{
    public class GridInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}