using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGrid
{
    public class GridController : MonoBehaviour
    {
        public List<Collider2D> floorColliders = new List<Collider2D>();
        public List<Collider2D> pathColliders = new List<Collider2D>();
        public List<Collider2D> wallColliders = new List<Collider2D>();
        public List<Collider2D> pillarColliders = new List<Collider2D>();
    }
}
