using System.Collections.Generic;
using UnityEngine;

namespace Roulettes
{
    public class EnvironmentRoulette
    {
        public class EnvironmentEntity
        {
            public string type;
            public EnvironmentKind kind;
            public EnvironmentModifier modifier;
        }

        // if once you want a seed for randomization
        // private readonly System.Random randomizer = new System.Random(42);

        public Dictionary<EnvironmentKind, EnvironmentEntity> environmentKindsMap = new Dictionary<EnvironmentKind, EnvironmentEntity>();
        public EnvironmentEntity currentEntity;

        public EnvironmentRoulette()
        {
            CreateEnvironmentEntities();
            // AssignFullRandomModifiers();
            AssignRandomModifier();

            // PrintCurrentEntity();
        }

        public void NextRoll()
        {
            ResetModifiers();

            AssignRandomModifier();

            PrintCurrentEntity();
        }

        void CreateEnvironmentEntities()
        {
            foreach (EnvironmentKind kind in EnvironmentKind.GetValues(typeof(EnvironmentKind)))
            {
                if (kind == EnvironmentKind.Unknown) continue;

                environmentKindsMap.Add(kind, new EnvironmentEntity { type = "environment", kind = kind, modifier = EnvironmentModifier.Unchanged });
            }
        }

        void ResetModifiers()
        {
            foreach (var entity in environmentKindsMap)
            {
                entity.Value.modifier = EnvironmentModifier.Unchanged;
            }
        }

        void AssignRandomModifier()
        {
            bool useRandom = false;
            if (useRandom)
            {
                var randomModifier = (EnvironmentKind)Random.Range(1, EnvironmentKind.GetNames(typeof(EnvironmentKind)).Length);  // from 1, because of Unknown
                var randomEffect = (EnvironmentModifier)Random.Range(0, EnvironmentModifier.GetNames(typeof(EnvironmentModifier)).Length);
                // var randomModifier = (EnvironmentKind)randomizer.Next(1, EnvironmentKind.GetNames(typeof(EnvironmentKind)).Length);  // from 1, because of Unknown
                // var randomEffect = (EnvironmentModifier)randomizer.Next(0, EnvironmentModifier.GetNames(typeof(EnvironmentModifier)).Length);
                environmentKindsMap[randomModifier].modifier = randomEffect;

                currentEntity = new EnvironmentEntity()
                {
                    type = "environment",
                    kind = randomModifier,
                    modifier = randomEffect,
                };
            }
            else
            {
                Debug.LogWarning("ENVIRONMENT ROULETTE IS USING PRESET VALUES");
                // environmentPreset1
                environmentKindsMap[EnvironmentKind.Floor].modifier = EnvironmentModifier.Damage;
            }
        }

        void AssignFullRandomModifiers()
        {
            bool useRandom = true;
            if (useRandom)
            {
                foreach (var entity in environmentKindsMap)
                {
                    entity.Value.modifier = (EnvironmentModifier)Random.Range(0, EnvironmentModifier.GetNames(typeof(EnvironmentModifier)).Length);
                }
            }
            else
            {
                Debug.LogWarning("ENVIRONMENT ROULETTE IS USING PRESET VALUES");
                // environmentPreset1
                environmentKindsMap[EnvironmentKind.Path].modifier = EnvironmentModifier.Damage;
            }
        }

        void PrintCurrentEntity()
        {
            Debug.Log($"environmentEntity.kind: {currentEntity.kind}, environmentEntity.modifier: {currentEntity.modifier}");
        }

        void PrintAllEntities()
        {
            foreach (var entity in environmentKindsMap)
            {
                Debug.Log($"environmentEntity.kind: {entity.Value.kind}, environmentEntity.modifier: {entity.Value.modifier}");
            }
        }
    }
}
