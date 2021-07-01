using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    internal class AsteroidEnemy : MonoBehaviour, IMovableObject
    {
        public TypeOfTarget PossibleCollisions { get; set; } = TypeOfTarget.Player | TypeOfTarget.Ufo;
        public TypeOfTarget Type { get;  set; } = TypeOfTarget.Asteroid;
        public Generation Generation { get; set; }

        public static int AsteroidsCount;

        public void Move(float maxSpeed, float minSpeed)
        {
            AsteroidsCount++;
            
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
        }
    }
}