using System;
using UnityEngine;

namespace Source
{
    public class PlayerMovement : MonoBehaviour, IMovableObject
    {
        [SerializeField] private float stopSpeed = 5f;
        [SerializeField] private float startSpeed = 1f;
        [SerializeField] private float rotationSpeed = 360f;
        private Rigidbody2D _rigidbody;
        public static Vector3 CurrentPlayerPosition { get; private set; }

        public TypesOfTarget PossibleCollisions { get; set; } = TypesOfTarget.Asteroid | TypesOfTarget.Ufo;
        public TypesOfTarget Type { get; set; } = TypesOfTarget.Player;
        public Generation Generation { get; set; } = 0;

        public void DestroyEnemy()
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move(stopSpeed, startSpeed);
            Rotate();
        }

        public void Move(float maxSpeed, float minSpeed)
        {
            var force = transform.up * minSpeed;
            var currentSpeed = _rigidbody.velocity.magnitude;
            
            if (currentSpeed > maxSpeed)
            {
                force = Vector3.zero;
            }
            
            if (Input.GetKey(KeyCode.W)) 
                _rigidbody.AddForce(force);
            
            CurrentPlayerPosition = transform.position;
        }

        private void Rotate()
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            rotation *= Time.deltaTime;
            transform.Rotate(0, 0, -rotation);
        }
        
        public void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollision(collision, PossibleCollisions);
        }
    }
}