using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Source
{
    internal class GameProcess : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private int delayToNextRound = 3;
        [SerializeField] private PlayerMovement playerPrefab;
        [SerializeField] private int startNumOfAsteroids = 4;    
        [SerializeField] private int ufoIntervalSpawn = 3;
        [SerializeField] private int maxPlayerHealth = 5;
        
        private PlayerMovement _player;
        private int _currentRound;
        private int _currentPlayerHealth;

        private void Start()
        {
            HighScore.LoadHighScore();
            SoundsComponent.Sounds.PlayMainThemeMusic(true);
        }

        public void StartNewGame()
        {
            _currentPlayerHealth = maxPlayerHealth;
            StartNextRound();
            uiManager.StartGameScreen(maxPlayerHealth);
            StartCoroutine(nameof(CheckEndRound));
        }
        
        private IEnumerator CheckEndRound()
        {
            while (AsteroidEnemy.AsteroidsCount > 0)
                yield return null;
            
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
            
            _currentPlayerHealth--;
            
            uiManager.RemoveHealth();
            
            if (_currentPlayerHealth < 1)
            {
                uiManager.StartLoseScreen();
                var scene = SceneManager.GetActiveScene();
                
                yield return new WaitForSeconds(delayToNextRound);
                    
                SceneManager.LoadScene(scene.name);
                yield break;
            }
            
            yield return new WaitForSeconds(delayToNextRound);
            
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
            uiManager.SetCurrentRound(_currentRound);
            
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
            var gen = Random.Range(0, 2);
            
            EnemySpawner.SpawnEnemy<UfoEnemy>(EnemySpawner.GetUfoSpawnPos(), gen);
            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
    }
}