using Asteroids.Source;
using Source.EnemySource;
using Source.Interfaces;
using Source.Services;
using UnityEngine;

namespace Source.ActorSupports
{
    public static class CollisionExtension
    {
        public static void OnCollision(this MonoBehaviour initiator, Collider2D injured, PossibleCollisions possibleCollisions)
        {
            if (injured.TryGetComponent(out IMovableObject hit))
            {
                if (possibleCollisions.HasFlag(hit.Type))
                {
                    switch (hit.Type)
                    {
                        case PossibleCollisions.Asteroid:
                            AsteroidActor.AsteroidsCount--;

                            var genAsteroid = (int) injured.GetComponent<AsteroidActor>().AsteroidGeneration;
                            var position = injured.transform.position;
                            // EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            // EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                            
                            SoundsComponent.Sounds.PlayAsteroidExplosionSound();
                            break;
                    
                        case PossibleCollisions.Ufo:
                            SoundsComponent.Sounds.PlayUfoDeathSound();
                            break;
                        case PossibleCollisions.Player:
                            SoundsComponent.Sounds.PlayHeroDeathSound();
                            break;
                    }
                    
                    HighScore.AddPoints(hit);
                    Object.Destroy(injured.gameObject);
                    Object.Destroy(initiator.gameObject);
                    Object.Instantiate(Explosion.Prefab, injured.transform.position, Quaternion.identity);
                    Object.Instantiate(Explosion.Prefab, initiator.transform.position, Quaternion.identity);
                }
            }
        }
    }
}