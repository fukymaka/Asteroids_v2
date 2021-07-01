using System;
using UnityEngine;

namespace Source
{
    public class PlayerMovement : MonoBehaviour, IMovableObject
    {
        [SerializeField] private float stopSpeed = 5f;
        [SerializeField] private float startSpeed = 1f;
        [SerializeField] private float rotationSpeed = 360f;
        [SerializeField] private AudioSource thrustSound;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        public static Vector3 CurrentPlayerPosition { get; private set; }

        public TypeOfTarget PossibleCollisions { get; set; } = TypeOfTarget.Asteroid | TypeOfTarget.Ufo;
        public TypeOfTarget Type { get; set; } = TypeOfTarget.Player;
        public Generation Generation { get; set; } = 0;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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
            {
                _rigidbody.AddForce(force);
                _animator.SetBool("isThrust", true);
                thrustSound.gameObject.SetActive(true);
            }
            else
                thrustSound.gameObject.SetActive(false);


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