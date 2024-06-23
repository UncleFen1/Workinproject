using UnityEngine;

namespace PoolProperty
{
    public enum ModProrertyMir
    {
        Raise,
        Drop,
        Intact
    }
    public struct MirPoolProperty
    {
        public int Hash { get; set; }
        public int[] ChildrenHash { get; set; }
        public GameObject Object;
        //
        public float Wall { get; set; }//стена
        public float Floor { get; set; }//пол
        public float Tracks { get; set; }//дорожки
        public float Columns { get; set; }//колонны
        public float Statues { get; set; }//статуи
        public float Flame { get; set; }//факелы
    }
}