using System;
using TMPro;
using UnityEngine;

namespace Source
{
    public class ProjectileMovement : MonoBehaviour 
    {
        [HideInInspector] public Vector2 from;
        [HideInInspector] public Vector2 to;
        [HideInInspector] public float speed;
        [HideInInspector] public TypesOfTarget targets;

        private void Update()
        {
            Fire();
        }

        private void Fire()
        {
            var directionProjectile = (to - from).normalized;
            transform.Translate(directionProjectile * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollision(collision, targets);
        }
    }
}