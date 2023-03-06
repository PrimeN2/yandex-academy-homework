using UnityEngine;
using UnityEngine.UI;

public class CompositionRoot : MonoBehaviour
{
	[SerializeField] private CoinsPresenter _coinsPresenter;
	[SerializeField] private Text _coinsLabel;
	[SerializeField] private Animator _coinsAnimator;

	private Wallet _wallet;
	private ISaveSystem _saveSystem;

	private void Awake()
	{
		Bootstrap();
	}

	private void OnEnable()
	{
		_wallet.OnBalanceChanged += _saveSystem.Save;
	}

	private void OnDisable()
	{
		_wallet.OnBalanceChanged -= _saveSystem.Save;
	}

	public void Bootstrap()
	{
		SetSaveSystem();
		SetEconomics();
	}

	private void SetEconomics()
	{
		_wallet = new Wallet(_saveSystem.LoadBalance());
		_coinsPresenter.Construct(_wallet, _coinsLabel, _coinsAnimator);
	}

	private void SetSaveSystem()
	{
		_saveSystem = new PlayerPrefsSaveSystem();
	}
}