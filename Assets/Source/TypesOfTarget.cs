using System;

namespace Source
{
    [Flags]
    public enum TypesOfTarget
    {
        None = 0,
        Asteroid = 1,
        Ufo = 2,
        Player = 4
    }
}