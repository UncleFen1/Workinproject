using System.Collections.Generic;
using GameGrid;
using UnityEngine;

namespace GameEnvironmentIntersection
{
    public class SharedEnvironmentIntersection
    {
        public static bool CheckColliderHitWallOrPillar(Collider2D collider, List<GridController> gcs)
        {
            var instanceId = collider.GetInstanceID();
            foreach (var gridController in gcs)
            {
                var wallColliders = gridController.wallColliders;
                var pillarColliders = gridController.pillarColliders;

                bool isFound = false;
                if (!isFound)
                {
                    foreach (var col in wallColliders)
                    {
                        if (instanceId == col.GetInstanceID())
                        {
                            isFound = true;
                            break;
                        }
                    }
                }
                if (!isFound)
                {
                    foreach (var col in pillarColliders)
                    {
                        if (instanceId == col.GetInstanceID())
                        {
                            isFound = true;
                            break;
                        }
                    }
                }

                if (isFound)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
