using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Source
{
    [CreateAssetMenu(fileName = "Enemy_type_", menuName = "Create Enemy Type", order = 1)]
    public class EnemySettings : ScriptableObject
    {
        public string nameEnemy;

        public List<GameObject> enemyGeneration;
        
        public int minSpeed;
        public int maxSpeed;
    }
}