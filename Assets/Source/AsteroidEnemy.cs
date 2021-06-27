using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class AsteroidEnemy : MonoBehaviour, ILoveEnemy
    {
        public TypesOfTarget Types { get; set; }

        private void Start()
        {
            Types = TypesOfTarget.Enemy;
            Debug.Log("PRIVET");
        }

        public void Move(float maxSpeed, float minSpeed)
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
        }

        public void DestroyEnemy()
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            
        }
    }
}