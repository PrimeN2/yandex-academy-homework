using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameCycle : MonoBehaviour
{
	public void Restart(float delay)
	{
		StartCoroutine(DelayedRestart(delay));
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private IEnumerator DelayedRestart(float time)
	{
		yield return new WaitForSeconds(time);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}