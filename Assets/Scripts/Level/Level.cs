using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    [CreateAssetMenu(fileName = "new level", menuName = "level", order = 50)]
    public class Level : ScriptableObject
    {
        [SerializeField] private Vector2 _spawnPoint = new Vector2(0, 0);
        public Vector2 SpawnPoint => _spawnPoint;
    }
}
