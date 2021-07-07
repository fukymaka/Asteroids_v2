using Asteroids.Source;
using Source.Interfaces;
using UnityEngine;

namespace Source.ActorSupports
{
    public class ProjectileMovement : MonoBehaviour, IActor
    {
        private BoundsControl _boundsControl;
        
        [HideInInspector] public Vector2 from;
        [HideInInspector] public Vector2 to;
        [HideInInspector] public float speed;
        // [HideInInspector] public PossibleCollisions targets;

        public ActorType ActorType { get; } = ActorType.Projectile;
        public PossibleCollisions PossibleCollisions { get; set; }

        private void Start()
        {
            _boundsControl = GetComponent<BoundsControl>();
        }

        private void Update()
        {
            Move();
            CheckBounds();
        }

        private void Move()
        {
            var directionProjectile = (to - from).normalized;
            transform.Translate(directionProjectile * (speed * Time.deltaTime));
        }

        private void CheckBounds()
        {
            if (_boundsControl.isBoundsOut)
                Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // this.OnCollision(collision, targets);
        }

        
    }
}