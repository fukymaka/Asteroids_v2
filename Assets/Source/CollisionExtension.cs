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
                            var genAsteroid = (int) injured.GetComponent<AsteroidEnemy>().Generation;
                            var position = injured.transform.position;
                            EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            GameObject.Destroy(injured.gameObject);
                            GameObject.Destroy(initiator.gameObject);
                            break;
                    
                        case TypesOfTarget.Ufo:
                        case TypesOfTarget.Player:
                            GameObject.Destroy(injured.gameObject);
                            GameObject.Destroy(initiator.gameObject);
                            break;
                    }
                }
            }
        }
    }
}