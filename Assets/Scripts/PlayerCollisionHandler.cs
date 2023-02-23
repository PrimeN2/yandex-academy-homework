using UnityEngine;

[RequireComponent(typeof(PlayerJointsSystem))]
public class PlayerCollisionHandler : MonoBehaviour
{
	private PlayerMovement _playerMovement;

	private void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Rope rope;

		if (!_playerMovement.IsTied && other.TryGetComponent(out rope))
		{
			_playerMovement.TieTo(rope);

		}
	}
}