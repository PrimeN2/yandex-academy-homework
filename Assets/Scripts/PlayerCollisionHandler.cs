using UnityEngine;

[RequireComponent(typeof(PlayerJointsHandler))]
public class PlayerCollisionHandler : MonoBehaviour
{
	private PlayerJointsHandler _playerJointsHandler;

	private void Awake()
	{
		_playerJointsHandler = GetComponent<PlayerJointsHandler>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Rope rope;

		if (!_playerJointsHandler.IsTied && other.TryGetComponent(out rope))
		{
			_playerJointsHandler.TieJoint(rope);

		}
	}
}