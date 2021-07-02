using UnityEngine;

namespace Source
{
    public static class CollisionExtension
    {
        public static void OnCollision(this MonoBehaviour initiator, Collider2D injured, TypeOfTarget possibleCollisions)
        {
            if (injured.TryGetComponent(out IMovableObject hit))
            {
                if (possibleCollisions.HasFlag(hit.Type))
                {
                    switch (hit.Type)
                    {
                        case TypeOfTarget.Asteroid:
                            AsteroidEnemy.asteroidsCount--;

                            var genAsteroid = (int) injured.GetComponent<AsteroidEnemy>().Generation;
                            var position = injured.transform.position;
                            EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            
                            SoundsComponent.Sounds.PlayAsteroidExplosionSound();
                            break;
                    
                        case TypeOfTarget.Ufo:
                            SoundsComponent.Sounds.PlayUfoDeathSound();
                            break;
                        case TypeOfTarget.Player:
                            SoundsComponent.Sounds.PlayHeroDeathSound();
                            break;
                    }
                    
                    HighScore.AddPoints(hit);
                    Object.Destroy(injured.gameObject);
                    Object.Destroy(initiator.gameObject);
                    Object.Instantiate(Explosion.prefab, injured.transform.position, Quaternion.identity);
                    Object.Instantiate(Explosion.prefab, initiator.transform.position, Quaternion.identity);
                }
            }
        }
    }
}