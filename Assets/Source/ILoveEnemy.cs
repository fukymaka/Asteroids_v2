namespace Source
{
    public interface ILoveEnemy
    {
        TypesOfTarget Type { get; set; }
        void Move(float maxSpeed, float minSpeed);
        void DestroyEnemy();
    }
}