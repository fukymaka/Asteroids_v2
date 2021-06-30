using UnityEngine;

namespace Source
{
    public interface IMovableObject
    {
        TypesOfTarget PossibleCollisions { get; set; }
        TypesOfTarget Type { get; set; }
        Generation Generation { get; set; }
        void Move(float maxSpeed, float minSpeed);
    }
}