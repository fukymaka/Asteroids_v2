using Source.ActorSupports;

namespace Source.Interfaces
{
    public interface IMovableObject
    {
        PossibleCollisions PossibleCollisions { get; set; }
        PossibleCollisions Type { get; set; }
        

        // void Init() todo
        // void Move(float maxSpeed, float minSpeed);
    }
}