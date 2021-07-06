using UnityEngine;

namespace Asteroids.Source
{
    public class PrefabsHolder : MonoBehaviour
    {
        [Header("Player")] 
        [SerializeField] private PlayerMovement player;
        [Header("Asteroids")]
        [SerializeField] private AsteroidEnemy asteroidFirstGeneration;
        [SerializeField] private AsteroidEnemy asteroidSecondGeneration;
        [SerializeField] private AsteroidEnemy asteroidThirdGeneration;
        [Header("Ufo")]
        [SerializeField] private UfoEnemy ufoFirstType;
        [SerializeField] private UfoEnemy ufoSecondType;
        [SerializeField] private ProjectileMovement ufoProjectile;


        public AsteroidEnemy GetAsteroidPrefab(Generation generation)
        {
            switch (generation)
            {
                case Generation.First:
                    return asteroidFirstGeneration;
                case Generation.Second:
                    return asteroidSecondGeneration;
                case Generation.Third:
                    return asteroidThirdGeneration;
                default:
                    Debug.Log("Generation doesn't exist!");
                    return null;
            }
        }

        public UfoEnemy GetUfoPrefab(UfoType ufoType)
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