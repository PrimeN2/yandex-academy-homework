using UnityEngine;

[RequireComponent(typeof(PlayerTriggerHandler), typeof(CharacterMovement))]
public class Player : MonoBehaviour
{
	private CharacterMovement _playerMovement;
	private InputHandler _playerInputHandler;

	public PlayerTriggerHandler TriggerHandler { get; private set; }

	public void Construct(InputHandler playerInputHandler)
	{
		TriggerHandler = GetComponent<PlayerTriggerHandler>();
		_playerMovement = GetComponent<CharacterMovement>();

		_playerInputHandler = playerInputHandler;

		TriggerHandler.Construct(_playerInputHandler);
		_playerMovement.Construct(_playerInputHandler);
	}
}