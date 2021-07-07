using UnityEngine;

namespace Source.EnemySource
{
    [CreateAssetMenu(fileName = "Ufo_settings_", menuName = "Create Ufo settings", order = 2)]
    public class UfoSettings : ScriptableObject
    {
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float projectileSpeed;
        
        public float MinSpeed => minSpeed;
        public float MaxSpeed => maxSpeed;
        public float ProjectileSpeed => projectileSpeed;
    }
}