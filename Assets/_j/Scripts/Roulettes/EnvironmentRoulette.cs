using System.Collections.Generic;
using UnityEngine;
using EnvironmentUtils;

// TODO _j no need to be MonoBehaviour, it's only a background logic
public class EnvironmentRoulette : MonoBehaviour
{
    public class EnvironmentEntity
    {
        public string type;
        public EnvironmentKind kind;
        public EnvironmentModifier modifier;
    }

    public Dictionary<EnvironmentKind, EnvironmentEntity> environmentKindsMap = new Dictionary<EnvironmentKind, EnvironmentEntity>();


    void Start()
    {
        Init();
    }

    void Init()
    {
        CreateEnvironmentEntities();
        AssignRandomModifiers();

        PrintEnvironmentEntities();

        ApplyPropertiesOnGameobjects();
    }

    void ApplyPropertiesOnGameobjects() {
        // TODO GOs input hardlinked
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
