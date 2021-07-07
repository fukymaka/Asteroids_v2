using System.Collections;
using Asteroids.Source;
using Source.ActorSupports;
using Source.EnemySource;
using Source.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Source.Services
{
    internal class GameProcess : MonoBehaviour
    {
        [SerializeField] private PrefabsHolder prefabsHolder;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private DestroyProcessor destroyProcessor;
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private int delayToNextRound = 3;
        [SerializeField] private int startNumOfAsteroids = 4;    
        [SerializeField] private int ufoIntervalSpawn = 3;
        [SerializeField] private int maxPlayerHealth = 5;
        
        private PlayerActor _player;
        private int _currentRound;
        private int _currentPlayerHealth;

        private void Start()
        {
            AsteroidActor.AsteroidsCount = 0;
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
            while (AsteroidActor.AsteroidsCount > 0)
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
            // Destroy(EnemySpawner.EnemysAnchor);
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

            _player = Instantiate(prefabsHolder.Player);
            _player.SetMovementSettings(playerSettings.StartSpeed, playerSettings.MaxSpeed, playerSettings.RotationSpeed);
            _player.SetShootingSettings(prefabsHolder.PlayerProjectile, playerSettings.ProjectileSpeed);
            _player.DestroyProcessor = destroyProcessor;
        } 
        
        private void StartSpawnEnemys(int numAsteroids)
        {
            for (int i = 0; i < numAsteroids; i++)
                enemySpawner.SpawnAsteroid(default, AsteroidGeneration.First);

            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
        
        private void SpawnUfo()
        {
            var typeForSpawn = Random.Range(1, 3);
            
            enemySpawner.SpawnUfo((UfoType) typeForSpawn);
            Invoke(nameof(SpawnUfo), ufoIntervalSpawn);
        }
    }
}