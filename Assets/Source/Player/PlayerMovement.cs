using Source.ActorSupports;
using Source.Interfaces;
using UnityEngine;

namespace Source.Player
{
    public class PlayerMovement : MonoBehaviour, IActor, ICollision
    {
        [SerializeField] private float stopSpeed = 5f;
        [SerializeField] private float startSpeed = 1f;
        [SerializeField] private float rotationSpeed = 360f;
        [SerializeField] private AudioSource thrustSound;
        
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        public static Vector3 CurrentPlayerPosition { get; private set; }

        public PossibleCollisions PossibleCollisions { get; } = PossibleCollisions.Asteroid | PossibleCollisions.Ufo;
        public ActorType ActorType { get; } = ActorType.Player;

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

        private void Move(float maxSpeed, float minSpeed)
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