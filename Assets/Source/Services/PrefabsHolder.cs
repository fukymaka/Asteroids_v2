using Source.ActorSupports;
using Source.EnemySource;
using Source.Player;
using UnityEngine;

namespace Source.Services
{
    public class PrefabsHolder : MonoBehaviour
    {
        [Header("Player")] 
        [SerializeField] private PlayerActor player;
        [SerializeField] private ProjectileActor playerProjectile;
        [Header("Asteroids")]
        [SerializeField] private AsteroidActor asteroidFirstGeneration;
        [SerializeField] private AsteroidActor asteroidSecondGeneration;
        [SerializeField] private AsteroidActor asteroidThirdGeneration;
        [Header("Ufo")]
        [SerializeField] private UfoActor ufoFirstType;
        [SerializeField] private UfoActor ufoSecondType;
        [SerializeField] private ProjectileActor ufoProjectile;
        [SerializeField] private Explosion explosion;
        
        public PlayerActor Player => player;
        public ProjectileActor PlayerProjectile => playerProjectile;
        public ProjectileActor UfoProjectile => ufoProjectile;
        public Explosion Explosion => explosion;
        
        public AsteroidActor GetAsteroidPrefab(AsteroidGeneration asteroidGeneration)
        {
            switch (asteroidGeneration)
            {
                case AsteroidGeneration.First:
                    return asteroidFirstGeneration;
                case AsteroidGeneration.Second:
                    return asteroidSecondGeneration;
                case AsteroidGeneration.Third:
                    return asteroidThirdGeneration;
                default:
                    Debug.Log("Generation doesn't exist!");
                    return null;
            }
        }

        public UfoActor GetUfoPrefab(UfoType ufoType)
        {
            switch (ufoType)
            {
                case UfoType.First:
                    return ufoFirstType;
                case UfoType.Second:
                    return ufoSecondType;
                default:
                    Debug.Log("Ufo type doesn't exist!");
                    return null;
            }
        }
    }
}