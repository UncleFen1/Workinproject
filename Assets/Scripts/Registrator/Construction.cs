using UnityEngine;

namespace Registrator
{
    public enum TypeData
    {
        GameLogic,
        TextLogic
    }
    public struct Construction : IConstruction
    {
        public int Hash { get; set; }
        public int[] ChildrenHash { get; set; }
        public GameObject Object;
        //
        public TypeData TypeData;

    }
}