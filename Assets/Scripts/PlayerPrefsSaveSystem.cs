using UnityEngine;

public class PlayerPrefsSaveSystem : ISaveSystem
{
	public const string CoinsSave = "Coins";

	public int LoadBalance()
	{
		return PlayerPrefs.GetInt(CoinsSave, 0);
	}

	public void Save(int balance)
	{
		PlayerPrefs.SetInt(CoinsSave, balance);
	}
}