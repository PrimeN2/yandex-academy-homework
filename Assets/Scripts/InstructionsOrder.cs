using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InstructionsOrder : MonoBehaviour
{
	[SerializeField] private Canvas _canvas;
	[SerializeField] private GameObject _dangerSignGuideline;
	[SerializeField] private GameObject _playerControllGuideline;
	[SerializeField] private GameObject _congratulationsLabel;

	private InputHandler _input;
	private LevelGenerator _levelGenerator;
	private GameCycle _gameCycle;
	private SaveSystem _saveSystem;

	public void Construct(
		LevelGenerator levelGenerator, InputHandler input, GameCycle gameCycle, SaveSystem saveSystem)
	{
		_canvas.worldCamera = Camera.main;
		_canvas.sortingLayerName = "Layer 3";
		_levelGenerator = levelGenerator;
		_input = input;
		_gameCycle = gameCycle;
		_saveSystem = saveSystem;

		_dangerSignGuideline.SetActive(true);
		StartCoroutine(ConductDangerSignGuide());
	}

	private IEnumerator ConductDangerSignGuide()
	{
		Time.timeScale = 0;

		yield return new WaitForSecondsRealtime(2);

		Time.timeScale = 1;

		while (_levelGenerator.FirstSection.Eagle.IsAware)
			yield return null;

		_dangerSignGuideline.SetActive(false);

		_canvas.sortingOrder = 4;

		_playerControllGuideline.SetActive(true);
		StartCoroutine(ConductPlayerControllGuide());
	}

	private IEnumerator ConductPlayerControllGuide()
	{
		_input.Construct(true);

		while (!_input.ShouldMove)
			yield return null;

		_playerControllGuideline.SetActive(false);

		StartCoroutine(WaitForPassingOneSection());
	}

	private IEnumerator WaitForPassingOneSection()
	{
		while (!_levelGenerator.HasPassedFirstSection)
			yield return null;

		_congratulationsLabel.SetActive(true);

		Time.timeScale = 0;

		yield return new WaitForSecondsRealtime(1f);

		Time.timeScale = 1;

		_saveSystem.PassGuide();
		_gameCycle.Restart();
	}
}