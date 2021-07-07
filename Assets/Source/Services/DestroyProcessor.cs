using System;
using Source.ActorSupports;
using Source.EnemySource;
using Source.Interfaces;
using UnityEngine;

namespace Source.Services
{
    public class DestroyProcessor : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private PrefabsHolder prefabsHolder;
        public void CheckCollision(IActor initiator, IActor injured)
        {
            var initiatorPossibleCollisions = initiator.PossibleCollisions;
            var injuredActorType = (PossibleCollisions) injured.ActorType;

            if (initiatorPossibleCollisions.HasFlag(injuredActorType))
            {
                initiator.DestroyThisActor();
                Instantiate(prefabsHolder.Explosion, initiator.CurrentPositon, Quaternion.identity);
            }
                
        }
        
        public void CheckAsteroidCollision(AsteroidActor initiator, IActor injured)
        {
            var initiatorPossibleCollisions = initiator.PossibleCollisions;
            var injuredActorType = (PossibleCollisions) injured.ActorType;

            if (initiatorPossibleCollisions.HasFlag(injuredActorType))
            {
                switch (initiator.AsteroidGeneration)
                {
                    case AsteroidGeneration.First:
                        enemySpawner.SpawnAsteroid(initiator.CurrentPositon, AsteroidGeneration.Second);
                        enemySpawner.SpawnAsteroid(initiator.CurrentPositon, AsteroidGeneration.Second);
                        break;
                    case AsteroidGeneration.Second:
                        enemySpawner.SpawnAsteroid(initiator.CurrentPositon, AsteroidGeneration.Third);
                        enemySpawner.SpawnAsteroid(initiator.CurrentPositon, AsteroidGeneration.Third);
                        break; 
                }
                
                HighScore.AddAsteroidPoints(initiator);
                
                initiator.DestroyThisActor();
                Instantiate(prefabsHolder.Explosion, initiator.CurrentPositon, Quaternion.identity);
            }
                

            
        }
    }
}