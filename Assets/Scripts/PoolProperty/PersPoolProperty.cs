using UnityEngine;

namespace PoolProperty
{
    public enum ModProrertyPers
    {
        Raise,
        Drop,
        Intact
    }
    public struct PersPoolProperty
    {
        public int Hash { get; set; }
        public int[] ChildrenHash { get; set; }
        public GameObject Object;
        //
        public float MeleeDamage { get; set; }//урон в ближнем бою
        public float RangedDamage { get; set; }//урон в дальнем бою
        public float AttackRange { get; set; }//дальность атаки
        public float AttackAccuracy { get; set; }//точность атаки
        public float KDBetweenAttacks { get; set; } //кд между атаками
        public float ExperienceGained { get; set; }//получаемый опыт
        public float Health { get; set; }//здоровье
        public float MovementSpeed { get; set; }//скорость передвижения
        public float CameraPositionHeight { get; set; }//высота положения камеры (зум)
    }
}