using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstraper : MonoBehaviour
{
	private const string RestartAnimation = "OnPlayerDied";

	[SerializeField] private LevelGenerator _levelGenerator;
	[SerializeField] private Follower _cameraFollower;
	[SerializeField] private InputHandler _playerInputHandler;
	[SerializeField] private ScoreLabel _scoreLabel;
	[SerializeField] private Animator _restartWindow;

	private SaveSystem _saveSystem;

	private void Start()
	{
		InitSaveSystem();

		if (_saveSystem.HasGuideBeenPassed)
			BootstrapGuide();
		else
			BootstrapGame();
	}
	private void OnDisable()
	{
		_levelGenerator.OnScoreChanged -= _scoreLabel.SetScore;
		_levelGenerator.Player.TriggerHandler.OnPlayerDied -= RestartScene;
	}

	private void BootstrapGuide()
	{
		
	}

	private void InitSaveSystem()
	{
		_saveSystem = new SaveSystem();
	}


	private void BootstrapGame()
	{
		BindInputHandler();
		GenerateLevel();
		FollowPlayerByCamera();
		BindPlayer();
		BindUI();
	}

	private void BindUI()
	{
		_scoreLabel.Construct(0);
		_levelGenerator.OnScoreChanged += _scoreLabel.SetScore;
	}

	private void BindPlayer()
	{
		_levelGenerator.Player.Construct(_playerInputHandler);
		_levelGenerator.Player.TriggerHandler.OnPlayerDied += RestartScene;
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
		_levelGenerator.Construct(0);
	}

	private void RestartScene()
	{
		_restartWindow.SetTrigger(RestartAnimation);

		StartCoroutine(DelayedRestart(0.3f));

	}

	private IEnumerator DelayedRestart(float time)
	{
		yield return new WaitForSeconds(time);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
