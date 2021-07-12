using System;
using Source.ActorSupports;
using Source.Interfaces;
using Source.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.EnemySource
{
    public class AsteroidActor : MonoBehaviour, IActor
    {
        public ActorType ActorType { get; } = ActorType.Asteroid;
        public PossibleCollisions PossibleCollisions { get; } = PossibleCollisions.Player | PossibleCollisions.Ufo | PossibleCollisions.PlayerProjectile;
        public Vector3 CurrentPositon { get; private set; }
        public AsteroidGeneration AsteroidGeneration { get; set; }

        public static int AsteroidsCount;
        public DestroyProcessor DestroyProcessor { get; set; }

        private void Update()
        {
            CurrentPositon = transform.position;
        }

        public void Move(float speed)
        {
            AsteroidsCount++;
            
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(transform.up * speed);
        }

        public void DestroyThisActor()
        {
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IActor injured)) 
                DestroyProcessor.CheckAsteroidCollision(this, injured);
        }
    }
}