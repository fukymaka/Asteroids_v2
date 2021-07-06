using UnityEngine;

namespace Asteroids.Source
{
    [CreateAssetMenu(fileName = "Asteroid_settings_", menuName = "Create Asteroid settings", order = 1)]
    public class AsteroidSettings : ScriptableObject
    {
        [SerializeField] private float asteroidSpeed;
        public float AsteroidSpeed => asteroidSpeed;
    }
}