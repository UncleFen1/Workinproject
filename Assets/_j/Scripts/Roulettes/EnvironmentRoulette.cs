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
            AssignRandomModifiers();

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

        void AssignRandomModifiers()
        {
            bool useRandom = true;
            if (useRandom)
            {
                foreach (var environmentEntity in environmentKindsMap)
                {
                    environmentEntity.Value.modifier = (EnvironmentModifier)Random.Range(0, EnvironmentModifier.GetNames(typeof(EnvironmentModifier)).Length);
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
            foreach (var environmentEntity in environmentKindsMap)
            {
                Debug.Log($"environmentEntity.kind: {environmentEntity.Value.kind}, environmentEntity.modifier: {environmentEntity.Value.modifier}");
            }
        }
    }
}
