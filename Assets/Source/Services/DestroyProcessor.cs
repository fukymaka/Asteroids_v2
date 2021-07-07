using System;
using Source.ActorSupports;
using Source.EnemySource;
using Source.Interfaces;
using UnityEngine;

namespace Source.Services
{
    public class DestroyProcessor : MonoBehaviour
    {
        public void CheckAsteroid(AsteroidActor initiator, IActor injured)
        {
            var possibleCollisions = initiator.PossibleCollisions;

            Debug.Log($"{(int) possibleCollisions}");
            Debug.Log($"{(int) injured.ActorType}");

            if (possibleCollisions.HasFlag((PossibleCollisions) injured.ActorType))
            {
                Destroy(initiator.gameObject);
                
                switch (injured.ActorType)
                {
                    case ActorType.Projectile:
                        Destroy(initiator);
                        break;
                    // Object.Destroy(initiator.gameObject);
                    // Object.Instantiate(Explosion.Prefab, injured.transform.position, Quaternion.identity);
                    // Object.Instantiate(Explosion.Prefab, initiator.transform.position, Quaternion.identity);

                    // case ActorType.Asteroid:
                    //     AsteroidActor.AsteroidsCount--;
                    //
                    //     var genAsteroid = (int) injured.GetComponent<AsteroidActor>().AsteroidGeneration;
                    //     var position = injured.transform.position;
                    //     // EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                    //     // EnemySpawner.SpawnEnemy<AsteroidEnemy>(position, genAsteroid + 1);
                    //         
                    //     SoundsComponent.Sounds.PlayAsteroidExplosionSound();
                    //     break;
                    //
                    // case ActorType.Ufo:
                    //     SoundsComponent.Sounds.PlayUfoDeathSound();
                    //     break;
                    // case ActorType.Player:
                    //     SoundsComponent.Sounds.PlayHeroDeathSound();
                    //     break;
                }
            }

        }
    }
}