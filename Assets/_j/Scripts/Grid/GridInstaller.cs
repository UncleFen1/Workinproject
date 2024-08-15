using System.Collections.Generic;
using Zenject;

namespace GameGrid
{
    public class GridInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            // Component vs ComponentS
            // Container.Bind<GridController>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<GridController>().FromComponentsInHierarchy().AsSingle().NonLazy();
        }
    }
}