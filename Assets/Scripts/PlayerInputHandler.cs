using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField] private PlayerJointsHandler _playerJointsHandler;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			_playerJointsHandler.UntieCurrentJoint();
		}
	}
}
