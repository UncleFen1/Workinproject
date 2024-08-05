using System.Collections.Generic;
using UnityEngine;

namespace Roulettes
{
    public class PlayerRoulette
    {
        public class PlayerEntity
        {
            public string type;
            public PlayerKind kind;
            public PlayerModifier modifier;
        }

        public Dictionary<PlayerKind, PlayerEntity> playerKindsMap = new Dictionary<PlayerKind, PlayerEntity>();

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
                    new PlayerEntity
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
                foreach (var playerEntity in playerKindsMap)
                {
                    playerEntity.Value.modifier = (PlayerModifier)Random.Range(0, PlayerModifier.GetNames(typeof(PlayerModifier)).Length);
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
}
