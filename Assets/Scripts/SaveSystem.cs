using UnityEngine;

public class SaveSystem
{
	private const string HasGuideBeenPassedSave = "HasGuideBeenPassed";

	public bool HasGuideBeenPassed { get => PlayerPrefs.GetInt(HasGuideBeenPassedSave, 0) != 0; }

	public void PassGuide()
	{
		PlayerPrefs.SetInt(HasGuideBeenPassedSave, 1);
	}

	public void DeleteSaves()
	{
		PlayerPrefs.SetInt(HasGuideBeenPassedSave, 0);
	}
}