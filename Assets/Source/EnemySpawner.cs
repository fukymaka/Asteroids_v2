using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source
{
    public class EnemySpawner 
    {
        private static float _clearZoneRadius = 3f;
        private static float _offsetBounds = 3f;
        
        public static GameObject EnemysAnchor;
        public static EnemySettings AsteroidsSettings;
        public static EnemySettings UfoSettings;

        public static void SpawnEnemy<T>(Vector2 startPos, int gen) where T : MonoBehaviour, IMovableObject
        {
            var currentSettings = SetCurrentEnemySettings<T>();
            
            if (currentSettings != null)
            {
                if (gen >= currentSettings.enemyGeneration.Count) return;

                var enemy = Object.Instantiate(currentSettings.enemyGeneration[gen], startPos, Quaternion.identity);
                var enemyComponent = enemy.AddComponent<T>();
                var speedMultiplier = gen == 0 ? 1 : 2;
                enemyComponent.Generation = (Generation) gen;
                
                //todo
                enemyComponent.Move(currentSettings.maxSpeed * speedMultiplier, currentSettings.minSpeed * speedMultiplier);
                SetAnchor(enemy);
            }
        }

        private static EnemySettings SetCurrentEnemySettings<T>() where T : MonoBehaviour, IMovableObject
        {
            var tempEnemy = new GameObject();
            var tempEnemyComponent = tempEnemy.AddComponent<T>();

            EnemySettings currentSettings = null;
            
            switch (tempEnemyComponent.Type)
            {
                case TypeOfTarget.Asteroid:
                    currentSettings = AsteroidsSettings;
                    break;
                case TypeOfTarget.Ufo:
                    currentSettings = UfoSettings;
                    break;
            }

            Object.Destroy(tempEnemy);

            return currentSettings;
        }

        private static void SetAnchor(GameObject enemy)
        {
            if (EnemysAnchor == null) 
                EnemysAnchor = new GameObject("EnemysAnchor");

            enemy.transform.SetParent(EnemysAnchor.transform);
        }
        
        public static Vector2 GetAsteroidSpawnPos()
        {
            var boundHeight = BoundsControl.BoundHeight;
            var boundWidth = BoundsControl.BoundWidth;

            var startPos = Vector2.zero;

            //todo
            while (Math.Abs(startPos.x) < _clearZoneRadius && Math.Abs(startPos.y) < _clearZoneRadius)
            {
                startPos.x = Random.Range(-boundWidth, boundWidth);
                startPos.y = Random.Range(-boundHeight, boundHeight);
            }
            
            return startPos;
        }

        public static Vector2 GetUfoSpawnPos()
        {
            var boundWidth = BoundsControl.BoundWidth;
            var boundHeight = BoundsControl.BoundHeight;
            var boundWidthWithOffset = BoundsControl.BoundWidth + _offsetBounds;
            var boundHeightWithOffset = BoundsControl.BoundHeight + _offsetBounds;

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