using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			_playerMovement.UntieFromRope();
		}
		if (Input.GetKey(KeyCode.Space))
		{
			_playerMovement.Jump();
		}
	}
}
