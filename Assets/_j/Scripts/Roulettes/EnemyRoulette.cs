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
        public EnemyEntity currentEntity;

        public EnemyRoulette()
        {
            CreateEnemyEntities();
            // AssignFullRandomModifiers();
            // AssignRandomModifier();

            // PrintCurrentEntity();
        }

        public void NextRoll() {
            ResetModifiers();

            AssignRandomModifier();

            PrintCurrentEntity();
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
            foreach (var entity in enemyKindsMap)
            {
                entity.Value.modifier = EnemyModifier.Unchanged;
            }
        }

        void AssignRandomModifier()
        {
            bool useRandom = true;
            if (useRandom)
            {
                var randomModifier = (EnemyKind)Random.Range(1, EnemyKind.GetNames(typeof(EnemyKind)).Length);  // from 1, because of Unknown
                var randomEffect = (EnemyModifier)Random.Range(0, EnemyModifier.GetNames(typeof(EnemyModifier)).Length);
                enemyKindsMap[randomModifier].modifier = randomEffect;

                currentEntity = new EnemyEntity()
                    {
                        // TODO probably type could be used for switch Cultist, Monster, ...
                        type = "enemy",
                        kind = randomModifier,
                        modifier = randomEffect,
                    };
            }
            else
            {
                Debug.LogWarning("ENEMY ROULETTE IS USING PRESET VALUES");
                // enemyPreset1
                enemyKindsMap[EnemyKind.MovementSpeed].modifier = EnemyModifier.Increased;
            }
        }

        void AssignFullRandomModifiers()
        {
            bool useRandom = true;
            if (useRandom)
            {
                foreach (var entity in enemyKindsMap)
                {
                    entity.Value.modifier = (EnemyModifier)Random.Range(0, EnemyModifier.GetNames(typeof(EnemyModifier)).Length);
                }
            }
            else
            {
                Debug.LogWarning("ENEMY ROULETTE IS USING PRESET VALUES");
                // enemyPreset1
                enemyKindsMap[EnemyKind.MovementSpeed].modifier = EnemyModifier.Increased;
            }
        }

        void PrintCurrentEntity()
        {
            Debug.Log($"enemyEntity.kind: {currentEntity.kind}, enemyEntity.modifier: {currentEntity.modifier}");
        }

        void PrintAllEntities()
        {
            foreach (var entity in enemyKindsMap)
            {
                if (entity.Value.modifier != EnemyModifier.Unchanged)
                {
                    Debug.Log($"enemyEntity.kind: {entity.Value.kind}, enemyEntity.modifier: {entity.Value.modifier}");
                }
            }
        }
    }
}
