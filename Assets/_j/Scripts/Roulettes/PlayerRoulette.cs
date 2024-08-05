using System.Collections.Generic;
using UnityEngine;
using PlayerUtils;

// TODO _j move to namespace Roulettes
public class PlayerRoulette
{
    public class EnenyEntity
    {
        public string type;
        public PlayerKind kind;
        public PlayerModifier modifier;
    }

    public Dictionary<PlayerKind, EnenyEntity> playerKindsMap = new Dictionary<PlayerKind, EnenyEntity>();

    public PlayerRoulette()
    {
        CreatePlayerEntities();
        AssignRandomModifiers();

        PrintPlayerEntities();
    }

    void CreatePlayerEntities()
    {
        foreach (PlayerKind kind in PlayerKind.GetValues(typeof(PlayerKind)))
        {
            if (kind == PlayerKind.Unknown) continue;

            playerKindsMap.Add(kind,
                new EnenyEntity
                {
                    // TODO _j don't like "type" as parameter name
                    type = "player",
                    kind = kind,
                    modifier = PlayerModifier.Unchanged,
                });
        }
    }

    void AssignRandomModifiers()
    {
        bool useRandom = true;
        if (useRandom)
        {
            foreach (var EnenyEntity in playerKindsMap)
            {
                EnenyEntity.Value.modifier = (PlayerModifier)Random.Range(0, PlayerModifier.GetNames(typeof(PlayerModifier)).Length);
            }
        }
        else
        {
            Debug.LogWarning("PLAYER ROULETTE IS USING PRESET VALUES");
            // playerPreset1
            playerKindsMap[PlayerKind.MovementSpeed].modifier = PlayerModifier.Increased;
        }
    }

    void PrintPlayerEntities()
    {
        foreach (var playerEntity in playerKindsMap)
        {
            Debug.Log($"playerEntity.kind: {playerEntity.Value.kind}, playerEntity.modifier: {playerEntity.Value.modifier}");
        }
    }
}
