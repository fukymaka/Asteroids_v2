using Source.ActorSupports;
using Source.Services;
using UnityEngine;

namespace Source.Interfaces
{
    public interface IActor
    {
        ActorType ActorType { get;}
        PossibleCollisions PossibleCollisions { get; }
        Vector3 CurrentPositon { get; }
        public DestroyProcessor DestroyProcessor { get; set; }
        void DestroyThisActor();
        void OnTriggerEnter2D(Collider2D collider);
    }
}