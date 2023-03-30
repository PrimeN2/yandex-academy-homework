using UnityEngine;

internal class SaveSystem
{
	private const string HasGuideBeenPassedSave = "HasGuideBeenPassed";

	public bool HasGuideBeenPassed { get => PlayerPrefs.GetInt(HasGuideBeenPassedSave, 0) == 1; }

#if UNITY_EDITOR
	private void OnDisable()
	{
		PlayerPrefs.SetInt(HasGuideBeenPassedSave, 0);
	}
#endif
}