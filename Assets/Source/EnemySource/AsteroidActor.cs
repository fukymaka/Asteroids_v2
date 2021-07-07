using System;
using Asteroids.Source;
using Source.ActorSupports;
using Source.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.EnemySource
{
    public class AsteroidActor : MonoBehaviour, IActor, ICollision
    {
        public ActorType ActorType { get; } = ActorType.Asteroid;
        public PossibleCollisions PossibleCollisions { get; } = PossibleCollisions.Player | PossibleCollisions.Ufo;
        public AsteroidGeneration AsteroidGeneration { get; set; }

        public static int AsteroidsCount;

        public void Move(float speed)
        {
            AsteroidsCount++;
            
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(transform.up * speed);
        }

        public delegate void CollisionHandler(AsteroidActor initiator, IActor injured);
        public event CollisionHandler AsteroidCollsion;


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IActor injured))
            {
                AsteroidCollsion?.Invoke(this, injured);
            }
        }
    }
    
    
}