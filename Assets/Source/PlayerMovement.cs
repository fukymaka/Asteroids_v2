using System;
using UnityEngine;

namespace Source
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float rotationSpeed = 360f;
        private Rigidbody2D _rigidbody;
        public static Vector3 CurrentPlayerPosition { get; private set; }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
            Rotate();
            
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.W)) 
                _rigidbody.AddForce(transform.up * moveSpeed);
            
            CurrentPlayerPosition = transform.position;
        }

        private void Rotate()
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            rotation *= Time.deltaTime;
            transform.Rotate(0, 0, -rotation);
        }
    }
}