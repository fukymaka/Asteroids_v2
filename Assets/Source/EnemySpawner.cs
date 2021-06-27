using UnityEngine;
namespace Source
{
    public class EnemySpawner : MonoBehaviour 
    {
        public static EnemySettings EnemySettings;
        
        public static void SpawnEnemy<T>(Vector2 startPos) where T : Component, ILoveEnemy
        {
            var enemy = Instantiate(EnemySettings.prefab, startPos, Quaternion.identity);
            enemy.AddComponent<T>().Move(EnemySettings.maxSpeed, EnemySettings.minSpeed);
        }
    }
}