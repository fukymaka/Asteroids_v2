using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class GameProcess : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;
        [SerializeField] private EnemySettings asteroidsSettings;
        [SerializeField] private EnemySettings ufoSettings;
        [SerializeField] private int startNumOfAsteroids = 4;

        [SerializeField] private GameObject ufoProjectilePrefab;
        [SerializeField] private int ufoProjectileSpeed = 3;

        
        private void Awake()
        {
            SetEnemySettings();
            SetUfoProjectile();
        }
        
        private void Start()
        {
            SpawnPlayer();
            StartSpawnEnemys(startNumOfAsteroids);
        }

        private void SpawnPlayer()
        {
            Instantiate(player);
        }

        private void SpawnUfo()
        {
            EnemySpawner.SpawnEnemy<UfoEnemy>(EnemySpawner.GetUfoSpawnPos(), 0);
            Invoke(nameof(SpawnUfo), 2);
        }

        private void SetEnemySettings()
        {
            EnemySpawner.AsteroidsSettings = asteroidsSettings;
            EnemySpawner.UfoSettings = ufoSettings;

            UfoEnemy.ProjectilePrefab = ufoProjectilePrefab;
            UfoEnemy.projectileSpeed = ufoProjectileSpeed;
        }

        private void StartSpawnEnemys(int numAsteroids)
        {
            for (int i = 0; i < numAsteroids; i++)
            {
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(EnemySpawner.GetAsteroidSpawnPos(), 0);
            }
            
            Invoke(nameof(SpawnUfo), 2);
        }

        private void SetUfoProjectile()
        {
            UfoEnemy.ProjectilePrefab = ufoProjectilePrefab;
        }
    }
}
