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
            foreach(var environmentEntity in environmentKindsMap)
            {
                environmentEntity.Value.modifier = (EnvironmentModifier) Random.Range(0, EnvironmentModifier.GetNames(typeof(EnvironmentModifier)).Length);
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
