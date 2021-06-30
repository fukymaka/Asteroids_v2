using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class AsteroidEnemy : MonoBehaviour, IMovableObject
    {
        public TypesOfTarget PossibleCollisions { get; set; } = TypesOfTarget.Player | TypesOfTarget.Ufo;
        public TypesOfTarget Type { get; set; } = TypesOfTarget.Asteroid;
        public Generation Generation { get; set; }
        public void Move(float maxSpeed, float minSpeed)
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
        }
    }
}