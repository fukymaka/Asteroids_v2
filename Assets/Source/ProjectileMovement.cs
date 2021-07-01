using System;
using TMPro;
using UnityEngine;

namespace Source
{
    public class ProjectileMovement : MonoBehaviour
    {
        private BoundsControl _boundsControl;
        
        [HideInInspector] public Vector2 from;
        [HideInInspector] public Vector2 to;
        [HideInInspector] public float speed;
        [HideInInspector] public TypesOfTarget targets;

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
            this.OnCollision(collision, targets);
        }
    }
}