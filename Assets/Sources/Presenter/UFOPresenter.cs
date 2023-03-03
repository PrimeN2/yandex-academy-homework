using Asteroids.Model;
using UnityEngine;

public class UFOPresenter : EnemyPresenter
{
	public UFO UFO { get => (UFO)_model; }

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		UFOPresenter enemy;

		if (collision.gameObject.TryGetComponent(out enemy))
		{
			UFO.Collide((UFO)enemy._model);
		}
	}
}