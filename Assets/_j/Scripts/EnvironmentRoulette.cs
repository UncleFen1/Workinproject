using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRoulette : MonoBehaviour
{
    // TODO _j Andrey, please distinguish walls and columns (pillars)

    public GameObject EnvironmentFloor;
    public GameObject EnvironmentWall;
    public GameObject EnvironmentRoad;


    public class EnvironmentEntity
    {
        public string type;
        public string kind;
        public string modifier;
    }

    // TODO _j maybe better to use ScriptableObjects and modify them from Editor?
    public List<string> EnvironmentKinds = new List<string>()
    {
        "wall",
        "ground",
        "road",
        "pillar",
        "statue",
        "torch"
    };

    public List<string> EnvironmentModifiers = new List<string>()
    {
        "unchanged",
        "damage",
        "abyss",
        "glow",
        "blowOut",
        "heal",
    };

    public List<EnvironmentEntity> environmentEntities = new List<EnvironmentEntity>();


    void Start()
    {
        init();
    }

    void init()
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
        foreach (var kind in EnvironmentKinds)
        {
            environmentEntities.Add(new EnvironmentEntity { type = "environment", kind = kind, modifier = null });
        }
    }

    void AssignRandomModifiers()
    {
        foreach (var environmentEntity in environmentEntities)
        {
            environmentEntity.modifier = EnvironmentModifiers[Random.Range(0, EnvironmentModifiers.Count)];
        }
    }

    void PrintEnvironmentEntities()
    {
        foreach (var environmentEntity in environmentEntities)
        {
            Debug.Log($"environmentEntity.kind: {environmentEntity.kind}, environmentEntity.modifier: {environmentEntity.modifier}");
        }
    }
}
