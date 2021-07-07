using Source.ActorSupports;

namespace Source.Interfaces
{
    public interface IActor
    {
        ActorType ActorType { get;}
        PossibleCollisions PossibleCollisions { get; }
    }
}