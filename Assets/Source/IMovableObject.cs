using UnityEngine;

namespace Asteroids.Source
{
    public interface IMovableObject
    {
        TypeOfTarget PossibleCollisions { get; set; }
        TypeOfTarget Type { get; set; }
        

        // void Init() todo
        // void Move(float maxSpeed, float minSpeed);
    }
}