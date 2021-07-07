using Source.ActorSupports;
using Source.Interfaces;
using Source.Services;
using UnityEngine;

namespace Source.Player
{
    public class PlayerActor : MonoBehaviour, IActor
    {
        [SerializeField] private AudioSource thrustSound;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        public float StartSpeed { get; private set; } = 1f;
        public float MaxSpeed { get; private set; } = 5f;
        public float RotationSpeed { get; private set; } = 360f;
        public ProjectileActor ProjectileActor { get; private set; }
        public float ProjectileSpeed { get; private set; } = 30f;
        public Vector3 CurrentPositon { get; private set; }
        public DestroyProcessor DestroyProcessor { get; set; }
        
        public PossibleCollisions PossibleCollisions { get; } = PossibleCollisions.Asteroid | PossibleCollisions.Ufo | PossibleCollisions.UfoProjectile;
        public ActorType ActorType { get; } = ActorType.Player;

        public static Vector3 CurrentPlayerPosition { get; private set; }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Move(MaxSpeed, StartSpeed);
            Rotate();
            CurrentPositon = transform.position;
        }

        public void SetMovementSettings(float startSpeed, float maxSpeed, float rotationSpeed)
        {
            StartSpeed = startSpeed;
            MaxSpeed = maxSpeed;
            RotationSpeed = rotationSpeed;
        }

        public void SetShootingSettings(ProjectileActor projectileActor, float projectileSpeed)
        {
            ProjectileActor = projectileActor;
            ProjectileSpeed = projectileSpeed;
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
            float rotation = Input.GetAxis("Horizontal") * RotationSpeed;
            rotation *= Time.deltaTime;
            transform.Rotate(0, 0, -rotation);
        }

        public void DestroyThisActor()
        {
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IActor injured)) 
                DestroyProcessor.CheckCollision(this, injured);
        }
    }
}