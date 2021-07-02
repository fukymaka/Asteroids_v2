using System;

namespace Source
{
    [Flags]
    public enum TypeOfTarget
    {
        None = 0,
        Asteroid = 1,
        Ufo = 2,
        Player = 4
    }
}