using System;
using System.Collections;
using UnityEngine;

namespace Source
{
    public class GameProcess : MonoBehaviour
    {
        [SerializeField] EnemySettings asteroidsSettings;
        [SerializeField] EnemySettings ufoSettings;
        [SerializeField] private int startNumOfAsteroids = 4;
        
        
        private void Start()
        {
            EnemySpawner.AsteroidsSettings = asteroidsSettings;
            EnemySpawner.UfoSettings = ufoSettings;
            
            for (int i = 0; i < startNumOfAsteroids; i++)
            {
                EnemySpawner.SpawnAsteroids(Vector2.zero, 0);
            }
            
            EnemySpawner.SpawnUfo(Vector2.zero, 0);
            EnemySpawner.SpawnUfo(Vector2.zero, 1);
            
        }
    }
}
