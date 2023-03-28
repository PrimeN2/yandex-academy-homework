using System;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
	[SerializeField] private Follower _cameraFollower;
	[SerializeField] private InputHandler _playerInputHandler;

	private void Start()
	{
		Bootstrap();
	}

	private void Bootstrap()
    {
		BindInputHandler();
		GenerateLevel();
		FollowPlayerByCamera();
		BindPlayer();
	}

	private void BindPlayer()
	{
		_levelGenerator.Player.Construct(_playerInputHandler);
	}

	private void BindInputHandler()
	{
		_playerInputHandler.Construct(true);
	}

	private void FollowPlayerByCamera()
	{
		_cameraFollower.Construct(_levelGenerator.Player.transform);
	}

	private void GenerateLevel()
	{
		_levelGenerator.Construct();
	}
}
