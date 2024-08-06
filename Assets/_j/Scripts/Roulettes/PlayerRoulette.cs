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
            // AssignRandomModifiers();
            AssignRandomModifier();

            PrintEntities();
        }

        public void NextRoll() {
            ResetModifiers();

            AssignRandomModifier();

            PrintEntities();
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

        void ResetModifiers()
        {
            foreach (var entity in playerKindsMap)
            {
                entity.Value.modifier = PlayerModifier.Unchanged;
            }
        }

        void AssignRandomModifier()
        {
            bool useRandom = true;
            if (useRandom)
            {
                // TODO _j Andrey, should we avoid Unchanged effect in case we only modify one kind
                var randomModifier = (PlayerKind)Random.Range(1, PlayerKind.GetNames(typeof(PlayerKind)).Length);  // from 1, because of Unknown
                var randomEffect = (PlayerModifier)Random.Range(1, PlayerModifier.GetNames(typeof(PlayerModifier)).Length);
                playerKindsMap[randomModifier].modifier = randomEffect;
            }
            else
            {
                Debug.LogWarning("PLAYER ROULETTE IS USING PRESET VALUES");
                // playerPreset1
                playerKindsMap[PlayerKind.MovementSpeed].modifier = PlayerModifier.Increased;
            }
        }

        void AssignFullRandomModifiers()
        {
            bool useRandom = true;
            if (useRandom)
            {
                foreach (var entity in playerKindsMap)
                {
                    entity.Value.modifier = (PlayerModifier)Random.Range(0, PlayerModifier.GetNames(typeof(PlayerModifier)).Length);
                }
            }
            else
            {
                Debug.LogWarning("PLAYER ROULETTE IS USING PRESET VALUES");
                // playerPreset1
                playerKindsMap[PlayerKind.MovementSpeed].modifier = PlayerModifier.Increased;
            }
        }

        void PrintEntities()
        {
            foreach (var entity in playerKindsMap)
            {
                if (entity.Value.modifier != PlayerModifier.Unchanged)
                {
                    Debug.Log($"playerEntity.kind: {entity.Value.kind}, playerEntity.modifier: {entity.Value.modifier}");
                }
            }
        }
    }
}
