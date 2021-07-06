using UnityEngine;

namespace Asteroids.Source
{
    [CreateAssetMenu(fileName = "Ufo_settings_", menuName = "Create Ufo settings", order = 2)]
    public class UfoSettings : ScriptableObject
    {
        [SerializeField] private int minSpeed;
        [SerializeField] private int maxSpeed;
        [SerializeField] private int projectileSpeed;
        
        public int MinSpeed => minSpeed;
        public int MaxSpeed => maxSpeed;
        public int ProjectileSpeed => projectileSpeed;
    }
}