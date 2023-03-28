using UnityEngine;

[RequireComponent(typeof(PlayerTriggerHandler), typeof(CharacterMovement))]
public class Player : MonoBehaviour
{
	private PlayerTriggerHandler _triggerHandler;
	private CharacterMovement _playerMovement;

	private InputHandler _playerInputHandler;

	public void Construct(InputHandler playerInputHandler)
	{
		 _triggerHandler = GetComponent<PlayerTriggerHandler>();
		_playerMovement = GetComponent<CharacterMovement>();

		_playerInputHandler = playerInputHandler;

		_triggerHandler.Construct(_playerInputHandler);
		_playerMovement.Construct(_playerInputHandler);
	}
}