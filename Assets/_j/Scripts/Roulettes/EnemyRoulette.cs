using System.Collections.Generic;
using UnityEngine;

namespace Roulettes
{
    // TODO _j create BaseRoulette abstract class
    public class EnemyRoulette
    {
        public class EnemyEntity
        {
            public string type;
            public EnemyKind kind;
            public EnemyModifier modifier;
        }

        public Dictionary<EnemyKind, EnemyEntity> enemyKindsMap = new Dictionary<EnemyKind, EnemyEntity>();

        public EnemyRoulette()
        {
            CreateEnemyEntities();
            AssignRandomModifiers();

            PrintEnemyEntities();
        }

        void CreateEnemyEntities()
        {
            foreach (EnemyKind kind in EnemyKind.GetValues(typeof(EnemyKind)))
            {
                if (kind == EnemyKind.Unknown) continue;

                enemyKindsMap.Add(kind,
                    new EnemyEntity
                    {
                        // TODO probably type could be used for switch Cultist, Monster, ...
                        type = "enemy",
                        kind = kind,
                        modifier = EnemyModifier.Unchanged,
                    });
            }
        }

        void ResetModifiers() {
            foreach (var enemyEntity in enemyKindsMap)
            {
                enemyEntity.Value.modifier = EnemyModifier.Unchanged;
            }
        }

        void AssignRandomModifiers()
        {
            bool useRandom = true;
            if (useRandom)
            {
                foreach (var enemyEntity in enemyKindsMap)
                {
                    enemyEntity.Value.modifier = (EnemyModifier)Random.Range(0, EnemyModifier.GetNames(typeof(EnemyModifier)).Length);
                }
            }
            else
            {
                Debug.LogWarning("ENEMY ROULETTE IS USING PRESET VALUES");
                // enemyPreset1
                enemyKindsMap[EnemyKind.MovementSpeed].modifier = EnemyModifier.Increased;
            }
        }

        void PrintEnemyEntities()
        {
            foreach (var enemyEntity in enemyKindsMap)
            {
                if (enemyEntity.Value.modifier != EnemyModifier.Unchanged)
                {
                    Debug.Log($"enemyEntity.kind: {enemyEntity.Value.kind}, enemyEntity.modifier: {enemyEntity.Value.modifier}");
                }
            }
        }
    }
}
