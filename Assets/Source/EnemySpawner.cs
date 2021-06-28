using UnityEngine;
namespace Source
{
    public static class EnemySpawner  
    {
        public static EnemySettings AsteroidsSettings;
        public static EnemySettings UfoSettings;
        
        public static void SpawnAsteroids(Vector2 startPos, int gen) 
        {
            if (gen >= AsteroidsSettings.enemyGeneration.Count)
            {
                Debug.Log("Generation is over");
                return;
            }

            var enemy = GameObject.Instantiate(AsteroidsSettings.enemyGeneration[gen], startPos, Quaternion.identity);
            enemy.AddComponent<AsteroidEnemy>().Move(AsteroidsSettings.maxSpeed * (gen + 1), AsteroidsSettings.minSpeed * (gen + 1));
            enemy.GetComponent<AsteroidEnemy>().Generation = (AsteroidGeneration) gen;
        }

        public static void SpawnUfo(Vector2 startPos, int gen)
        {
            var enemy = GameObject.Instantiate(UfoSettings.enemyGeneration[gen], startPos, Quaternion.identity);
            enemy.AddComponent<UfoEnemy>().Move(UfoSettings.maxSpeed * (gen + 1), UfoSettings.minSpeed * (gen + 1));
        }
    }
}