using UnityEngine;

namespace Source.Player
{
    [CreateAssetMenu(fileName = "Player_settings", menuName = "Create Player settings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float startSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float projectileSpeed;
        
        public float StartSpeed => startSpeed;
        public float MaxSpeed => maxSpeed;
        public float ProjectileSpeed => projectileSpeed;
        public float RotationSpeed => rotationSpeed;
    }
}