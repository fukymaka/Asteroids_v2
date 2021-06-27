namespace Source
{
    public interface ILoveEnemy
    {
        TypesOfTarget Types { get; set; }
        void Move(float maxSpeed, float minSpeed);
        void DestroyEnemy();
    }
}