namespace Asteroids.Model
{
    public interface IEnemyVisitor
    {
        void Visit(Enemy enemy);
        void Visit(Asteroid asteroid);
        void Visit(UFO nlo);
        void Visit(PartOfAsteroid nlo);
    }
}
