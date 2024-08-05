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

        public Dictionary<EnvironmentKind, EnvironmentEntity> environmentKindsMap = new Dictionary<EnvironmentKind, EnvironmentEntity>();


        public EnvironmentRoulette()
        {
            CreateEnvironmentEntities();
            // AssignFullRandomModifiers();
            AssignRandomModifier();

            PrintEnvironmentEntities();
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
            bool useRandom = true;
            if (useRandom)
            {
                // TODO _j Andrey, should we avoid Unchanged effect in case we only modify one kind
                var randomModifier = (EnvironmentKind)Random.Range(1, EnvironmentKind.GetNames(typeof(EnvironmentKind)).Length);  // from 1, because of Unknown
                var randomEffect = (EnvironmentModifier)Random.Range(1, EnvironmentModifier.GetNames(typeof(EnvironmentModifier)).Length);
                environmentKindsMap[randomModifier].modifier = randomEffect;
            }
            else
            {
                Debug.LogWarning("ENVIRONMENT ROULETTE IS USING PRESET VALUES");
                // environmentPreset1
                environmentKindsMap[EnvironmentKind.Path].modifier = EnvironmentModifier.Damage;
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

        void PrintEnvironmentEntities()
        {
            foreach (var entity in environmentKindsMap)
            {
                if (entity.Value.modifier != EnvironmentModifier.Unchanged)
                {
                    Debug.Log($"environmentEntity.kind: {entity.Value.kind}, environmentEntity.modifier: {entity.Value.modifier}");
                }
            }
        }
    }
}
