using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class GameProcess : MonoBehaviour
    {
        [SerializeField] EnemySettings asteroidsSettings;
        [SerializeField] EnemySettings ufoSettings;
        [SerializeField] private int startNumOfAsteroids = 4;


        private void Start()
        {
            Invoke(nameof(SpawnUfo), 2);
        }

        private void Awake()
        {
            EnemySpawner.AsteroidsSettings = asteroidsSettings;
            EnemySpawner.UfoSettings = ufoSettings;
            
            for (int i = 0; i < startNumOfAsteroids; i++)
            {
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(EnemySpawner.GetAsteroidSpawnPos(), 0);
            }
        }

        void SpawnUfo()
        {
            EnemySpawner.SpawnEnemy<UfoEnemy>(EnemySpawner.GetUfoSpawnPos(), 0);
            Invoke(nameof(SpawnUfo), 2);
        }

        
    }
}
