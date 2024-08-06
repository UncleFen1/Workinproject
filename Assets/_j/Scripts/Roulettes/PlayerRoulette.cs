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
        public PlayerEntity currentEntity;

        public PlayerRoulette()
        {
            CreatePlayerEntities();
            // AssignFullRandomModifiers();
            // AssignRandomModifier();

            // PrintCurrentEntity();
        }

        public void NextRoll() {
            ResetModifiers();

            AssignRandomModifier();

            PrintCurrentEntity();
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
                var randomModifier = (PlayerKind)Random.Range(1, PlayerKind.GetNames(typeof(PlayerKind)).Length);  // from 1, because of Unknown
                var randomEffect = (PlayerModifier)Random.Range(0, PlayerModifier.GetNames(typeof(PlayerModifier)).Length);
                playerKindsMap[randomModifier].modifier = randomEffect;

                currentEntity = new PlayerEntity() 
                {
                    type = "player",
                    kind = randomModifier,
                    modifier = randomEffect,
                };
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

        void PrintCurrentEntity()
        {
            Debug.Log($"playerEntity.kind: {currentEntity.kind}, playerEntity.modifier: {currentEntity.modifier}");
        }

        void PrintAllEntities()
        {
            foreach (var entity in playerKindsMap)
            {
                Debug.Log($"playerEntity.kind: {entity.Value.kind}, playerEntity.modifier: {entity.Value.modifier}");
            }
        }
    }
}
