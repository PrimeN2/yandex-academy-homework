using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CompositionRoot : MonoInstaller
{
	private Wallet _wallet;
	private ISaveSystem _saveSystem;

	public override void InstallBindings()
	{
		BindSaveSystem();

		BindEconomicServices();
	}

	private void OnEnable()
	{
		_wallet.OnBalanceChanged += _saveSystem.Save;
	}

	private void OnDisable()
	{
		_wallet.OnBalanceChanged -= _saveSystem.Save;
	}

	private void BindSaveSystem()
	{
		_saveSystem = new PlayerPrefsSaveSystem();

		Container
			.Bind<ISaveSystem>()
			.FromInstance(_saveSystem)
			.AsSingle();
	}

	private void BindEconomicServices()
	{
		_wallet = new Wallet(_saveSystem.LoadBalance());

		Container
			.Bind<Wallet>()
			.FromInstance(_wallet)
			.AsSingle();
	}
}
