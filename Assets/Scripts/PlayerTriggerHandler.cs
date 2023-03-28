using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
	private InputHandler _inputHandler;

	public void Construct(InputHandler inputHandler)
	{
		_inputHandler = inputHandler;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Eagle>(out _))
			Die();
	}

	private void Die()
	{
		_inputHandler.DisableHandling();
		Destroy(gameObject);
	}
}
