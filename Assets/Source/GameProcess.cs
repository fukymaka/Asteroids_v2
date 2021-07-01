using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source
{
    internal class GameProcess : MonoBehaviour
    {
        [SerializeField] private int delayToNextRound = 3;
        
        [SerializeField] private PlayerMovement player;
        [SerializeField] private int startNumOfAsteroids = 4;    
        [SerializeField] private int ufoIntervalSpawn = 3;
        private void Start()
        {
            SpawnPlayer();
            StartSpawnEnemys(startNumOfAsteroids);
        }

        
        private void Update()
        {
            if (AsteroidEnemy.AsteroidsCount < 1) 
                PreparationToNextRound(); //dont call this in update todo

            Debug.Log(AsteroidEnemy.AsteroidsCount);
        }

        private void PreparationToNextRound()
        {
            CancelInvoke();
            CleanScene();
            Invoke(nameof(StartNextRound), delayToNextRound);
            // StartNextRound();
        }
        
        private void CleanScene()
        {
            Destroy(EnemySpawner.EnemysAnchor);
        }

        private void StartNextRound()
        {
            player.transform.position = Vector3.zero;
            player.transform.rotation = Quaternion.identity;
            
            StartSpawnEnemys(startNumOfAsteroids * 2);
        }
        
        private void SpawnPlayer()
        {
            player = Instantiate(player);
        } 
        
        private void StartSpawnEnemys(int numAsteroids)
        {
            for (int i = 0; i < numAsteroids; i++)
            {
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(EnemySpawner.GetAsteroidSpawnPos(), 0);
            }
            
            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
        
        private void SpawnUfo()
        {
            EnemySpawner.SpawnEnemy<UfoEnemy>(EnemySpawner.GetUfoSpawnPos(), 0);
            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
    }
}