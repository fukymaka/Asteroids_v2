using Source.Interfaces;
using Source.Services;
using UnityEngine;

namespace Source.ActorSupports
{
    public class ProjectileActor : MonoBehaviour, IActor
    {
        private Vector2 _from;
        private Vector2 _to;
        private float _speed;
        public ActorType ActorType { get; set; } 
        public PossibleCollisions PossibleCollisions { get; set; }
        public DestroyProcessor DestroyProcessor { get; set; }
        public Vector3 CurrentPositon { get; private set; }
        
        private BoundsControl _boundsControl;

        private void Start()
        {
            _boundsControl = GetComponent<BoundsControl>();
        }

        private void Update()
        {
            Move();
            CheckBounds();
            CurrentPositon = transform.position;
        }

        public void InitSettings(ActorType actorType, PossibleCollisions possibleCollisions, DestroyProcessor destroyProcessor)
        {
            ActorType = actorType;
            PossibleCollisions = possibleCollisions;
            DestroyProcessor = destroyProcessor;
        }

        public void InitDirection(Vector2 from, Vector2 to, float speed)
        {
            _from = from;
            _to = to;
            _speed = speed;
        }

        private void Move()
        {
            var directionProjectile = (_to - _from).normalized;
            transform.Translate(directionProjectile * (_speed * Time.deltaTime));
        }

        private void CheckBounds()
        {
            if (_boundsControl.isBoundsOut)
                Destroy(gameObject);
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