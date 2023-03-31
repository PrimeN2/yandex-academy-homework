using UnityEngine;

public class Bootstraper : MonoBehaviour
{
	private const string RestartAnimation = "OnPlayerDied";

	[SerializeField] private LevelGenerator _levelGenerator;
	[SerializeField] private Follower _cameraFollower;
	[SerializeField] private InputHandler _playerInputHandler;
	[SerializeField] private ScoreLabel _scoreLabel;
	[SerializeField] private Animator _restartWindow;
	[SerializeField] private InstructionsOrder _instructions;
	[SerializeField] private GameCycle _gameCycle;

	private SaveSystem _saveSystem;

	private void Start()
	{
		InitSaveSystem();

		if (_saveSystem.HasGuideBeenPassed)
			BootstrapGame();
		else
			BootstrapGuide();
	}
	private void OnDisable()
	{
		_levelGenerator.OnScoreChanged -= _scoreLabel.SetScore;
		_levelGenerator.Player.TriggerHandler.OnPlayerDied -= Restart;

//#if UNITY_EDITOR
//		_saveSystem.DeleteSaves();
//#endif
	}

	private void BootstrapGuide()
	{
		GenerateLevel();
		GenerateInstructions();
		FollowPlayerByCamera();
		BindPlayer();
		BindUI();
	}

	private void GenerateInstructions()
	{
		var instructions = Instantiate(_instructions, transform);
		instructions.Construct(_levelGenerator, _playerInputHandler, _gameCycle, _saveSystem);
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
		_levelGenerator.Player.TriggerHandler.OnPlayerDied += Restart;
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

	private void Restart()
	{
		_restartWindow.SetTrigger(RestartAnimation);

		_gameCycle.Restart(0.3f);

	}
}
