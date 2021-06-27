using System;
using UnityEngine;

namespace Source
{
    public class GameProcess : MonoBehaviour
    {
        [SerializeField] EnemySettings asteroidsSettings;
        [SerializeField] private int startNumOfAsteroids = 4;
        
        
        private void Start()
        {
            EnemySpawner.EnemySettings = asteroidsSettings;
            
            for (int i = 0; i < startNumOfAsteroids; i++)
            {
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(Vector2.zero);
            }
        }
    }
}
