using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Asteroids.Source
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PrefabsHolder prefabsHolder;
        [SerializeField] private float clearZoneRadius = 3f;
        [SerializeField] private float offsetBounds = 3f;

        [SerializeField] private AsteroidSettings asteroidsSettings;
        [SerializeField] private UfoSettings ufoSettings;
        
        public GameObject asteroidContainer;
        public GameObject ufoContainer;

        public void SpawnAsteroid(Vector2 startPosition, Generation generation)
        {
            var asteroid = prefabsHolder.GetAsteroidPrefab(generation);
            asteroid = Instantiate(asteroid, GetAsteroidSpawnPos(), Quaternion.identity);
            asteroid.Generation = generation;
            var speedMultiplier = (int) generation;
            var asteroidSpeed = asteroidsSettings.AsteroidSpeed * speedMultiplier;
            asteroid.Move(asteroidSpeed * speedMultiplier);
            PutAsteroidInContainer(asteroid);
        }

        public void SpawnUfo(Vector2 startPosition, UfoType ufoType)
        {
            var ufo = prefabsHolder.GetUfoPrefab(ufoType);
            ufo = Instantiate(ufo, GetUfoSpawnPos(), Quaternion.identity);
            ufo.UfoType = ufoType;
            var speedMultiplier = (int) ufoType;
            var ufoMaxSpeed = ufoSettings.MaxSpeed * speedMultiplier;
            var ufoMinSpeed = ufoSettings.MinSpeed * speedMultiplier;

            ufo.Move(ufoMaxSpeed, ufoMinSpeed);
            PutUfoInContainer(ufo);
        }
        private void PutAsteroidInContainer(AsteroidEnemy asteroid)
        {
            if (asteroidContainer == null) 
                asteroidContainer = new GameObject("AsteroidContainer");

            asteroid.transform.SetParent(asteroidContainer.transform);
        }
        
        private void PutUfoInContainer(UfoEnemy ufo)
        {
            if (ufoContainer == null) 
                ufoContainer = new GameObject("UfoContainer");

            ufo.transform.SetParent(ufoContainer.transform);
        }
        
        public Vector2 GetAsteroidSpawnPos()
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

        public Vector2 GetUfoSpawnPos()
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