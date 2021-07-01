using UnityEngine;

namespace Source
{
    public static class CollisionExtension
    {
        public static void OnCollision(this MonoBehaviour initiator, Collider2D injured, TypesOfTarget possibleCollisions)
        {
            if (injured.TryGetComponent(out IMovableObject hit))
            {
                if (possibleCollisions.HasFlag(hit.Type))
                {
                    switch (hit.Type)
                    {
                        case TypesOfTarget.Asteroid:
                            AsteroidEnemy.AsteroidsCount--;
                            var genAsteroid = (int) injured.GetComponent<AsteroidEnemy>().Generation;
                            var position = injured.transform.position;
                            EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            break;
                    
                        case TypesOfTarget.Ufo:
                        case TypesOfTarget.Player:
                            break;
                    }
                    
                    Object.Destroy(injured.gameObject);
                    Object.Destroy(initiator.gameObject);
                    Object.Instantiate(Explosion.Prefab, injured.transform.position, Quaternion.identity);
                    Object.Instantiate(Explosion.Prefab, initiator.transform.position, Quaternion.identity);
                }
            }
        }
    }
}