using System.Collections.Generic;
using UnityEngine;
using EnemiesUtils;

public class EnemyRoulette
{
    public class EnenyEntity
    {
        public string type;
        public EnemyKind kind;
        public EnemyModifier modifier;
    }

    public Dictionary<EnemyKind, EnenyEntity> enemyKindsMap = new Dictionary<EnemyKind, EnenyEntity>();

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
                new EnenyEntity
                {
                    // TODO probably type could be used for switch Cultist, Monster, ...
                    type = "enemy",
                    kind = kind,
                    modifier = EnemyModifier.Unchanged,
                });
        }
    }

    void AssignRandomModifiers()
    {
        bool useRandom = true;
        if (useRandom)
        {
            foreach (var EnenyEntity in enemyKindsMap)
            {
                EnenyEntity.Value.modifier = (EnemyModifier)Random.Range(0, EnemyModifier.GetNames(typeof(EnemyModifier)).Length);
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
        foreach (var enenyEntity in enemyKindsMap)
        {
            Debug.Log($"enenyEntity.kind: {enenyEntity.Value.kind}, enenyEntity.modifier: {enenyEntity.Value.modifier}");
        }
    }
}
