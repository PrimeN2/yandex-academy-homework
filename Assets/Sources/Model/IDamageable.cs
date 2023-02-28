namespace Asteroids.Model
{
	public interface IDamageable
	{
		int CurrentHealth { get; }

		void TakeDamage(int damage);
	}
}
