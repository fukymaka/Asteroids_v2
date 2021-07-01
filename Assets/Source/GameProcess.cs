using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Source
{
    internal class GameProcess : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;
        
        [SerializeField] private int delayToNextRound = 3;
        
        [SerializeField] private PlayerMovement playerPrefab;
        [SerializeField] private int startNumOfAsteroids = 4;    
        [SerializeField] private int ufoIntervalSpawn = 3;
        [SerializeField] private int maxPlayerHealth = 2;
        
        private PlayerMovement _player;
        private int _currentRound;
        private int _currentPlayerHealth;
        
        private void Start()
        {
            _currentPlayerHealth = maxPlayerHealth;
            StartNextRound();
            StartCoroutine(nameof(CheckEndRound));
        }

        private IEnumerator CheckEndRound()
        {
            while (AsteroidEnemy.AsteroidsCount > 0)
            {
                Debug.Log($"AsteroidEnemy.AsteroidsCount {AsteroidEnemy.AsteroidsCount}");
                yield return null;
            }
            
            Debug.Log("NExtROund!");

            CancelInvoke();
            CleanScene();
            
            yield return new WaitForSeconds(delayToNextRound);

            StartNextRound();
            StartCoroutine(nameof(CheckEndRound));
        }
        
        private IEnumerator CheckPlayerLife()
        {
            while (_player != null)
                yield return null;

            yield return new WaitForSeconds(delayToNextRound);

            _currentPlayerHealth--;
            
            if (_currentPlayerHealth < 1)
            {
                uiManager.StartLoseScreen();
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                yield break;
            }
            
            SpawnPlayer();
            StartCoroutine(nameof(CheckPlayerLife));
        }
        
        
        
        private void CleanScene()
        {
            Destroy(EnemySpawner.EnemysAnchor);
        }

        private void StartNextRound()
        {
            SpawnPlayer();
            
            StartSpawnEnemys(startNumOfAsteroids + _currentRound);
            _currentRound++;
            StartCoroutine(nameof(CheckPlayerLife));
        }
        
        private void SpawnPlayer()
        {
            if (_player != null)
                Destroy(_player.gameObject); 
            
            _player = Instantiate(playerPrefab);
        } 
        
        private void StartSpawnEnemys(int numAsteroids)
        {
            for (int i = 0; i < numAsteroids; i++)
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(EnemySpawner.GetAsteroidSpawnPos(), 0);

            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
        
        private void SpawnUfo()
        {
            EnemySpawner.SpawnEnemy<UfoEnemy>(EnemySpawner.GetUfoSpawnPos(), 0);
            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
    }
}