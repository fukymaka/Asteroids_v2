using System;
using Asteroids.Source;
using Source.ActorSupports;
using Source.EnemySource;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Services
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PrefabsHolder prefabsHolder;
        [SerializeField] private float clearZoneRadius = 3f;
        [SerializeField] private float offsetBounds = 3f;

        [SerializeField] private AsteroidSettings asteroidsSettings;
        [SerializeField] private UfoSettings ufoSettings;
        [SerializeField] private DestroyProcessor destroyProcessor;

        public GameObject asteroidContainer;
        public GameObject ufoContainer;

        public void SpawnAsteroid(Vector2 startPosition, AsteroidGeneration asteroidGeneration)
        {
            if (startPosition == default)
                startPosition = GetAsteroidSpawnPos();
            
            var asteroid = prefabsHolder.GetAsteroidPrefab(asteroidGeneration);
            asteroid = Instantiate(asteroid, startPosition, Quaternion.identity);
            asteroid.AsteroidGeneration = asteroidGeneration;
            
            var speedMultiplier = (int) asteroidGeneration;
            var asteroidSpeed = asteroidsSettings.AsteroidSpeed * speedMultiplier;
            asteroid.Move(asteroidSpeed * speedMultiplier);
            
            PutAsteroidInContainer(asteroid);

            asteroid.DestroyProcessor = destroyProcessor;
        }

        public void SpawnUfo(UfoType ufoType)
        {
            var ufo = prefabsHolder.GetUfoPrefab(ufoType);
            var startPosition = GetUfoSpawnPos();
            ufo = Instantiate(ufo, startPosition, Quaternion.identity);
            ufo.UfoType = ufoType;
            ufo.SetProjectilePrefab(prefabsHolder.UfoProjectile);
            ufo.SetProjectileSpeed(ufoSettings.ProjectileSpeed);
            
            var speedMultiplier = (int) ufoType;
            var ufoMaxSpeed = ufoSettings.MaxSpeed * speedMultiplier;
            var ufoMinSpeed = ufoSettings.MinSpeed * speedMultiplier;
            ufo.Move(ufoMaxSpeed, ufoMinSpeed);
            
            PutUfoInContainer(ufo);
            
            ufo.DestroyProcessor = destroyProcessor;
        }
        private void PutAsteroidInContainer(AsteroidActor asteroid)
        {
            if (asteroidContainer == null) 
                asteroidContainer = new GameObject("AsteroidContainer");

            asteroid.transform.SetParent(asteroidContainer.transform);
        }
        
        private void PutUfoInContainer(UfoActor ufo)
        {
            if (ufoContainer == null) 
                ufoContainer = new GameObject("UfoContainer");

            ufo.transform.SetParent(ufoContainer.transform);
        }

        private Vector2 GetAsteroidSpawnPos()
        {
            var boundHeight = BoundsControl.BoundHeight;
            var boundWidth = BoundsControl.BoundWidth;

            var startPos = Vector2.zero;

            //todo
            while (Math.Abs(startPos.x) < clearZoneRadius && Math.Abs(startPos.y) < clearZoneRadius)
            {
                startPos.x = Random.Range(-boundWidth, boundWidth);
                startPos.y = Random.Range(-boundHeight, boundHeight);
            }
            
            return startPos;
        }

        private Vector2 GetUfoSpawnPos()
        {
            var boundWidth = BoundsControl.BoundWidth;
            var boundHeight = BoundsControl.BoundHeight;
            var boundWidthWithOffset = BoundsControl.BoundWidth + offsetBounds;
            var boundHeightWithOffset = BoundsControl.BoundHeight + offsetBounds;

            var startPos = Vector2.zero;

            //todo
            while (Math.Abs(startPos.x) < boundWidth && Math.Abs(startPos.y) < boundHeight)
            {
                startPos.x = Random.Range(-boundWidthWithOffset, boundWidthWithOffset);
                startPos.y = Random.Range(-boundHeightWithOffset, boundHeightWithOffset);
            }
            
            return startPos;
        }
    }
}