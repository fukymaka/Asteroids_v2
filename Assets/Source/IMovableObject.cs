using UnityEngine;

namespace Source
{
    public interface IMovableObject
    {
        TypeOfTarget PossibleCollisions { get; set; }
        TypeOfTarget Type { get; set; }
        Generation Generation { get; set; }
        void Move(float maxSpeed, float minSpeed);
    }
}