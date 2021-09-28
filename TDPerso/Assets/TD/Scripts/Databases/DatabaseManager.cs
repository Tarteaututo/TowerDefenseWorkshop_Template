namespace GSGD1
{
	using UnityEngine;
	using UnityEngine.Assertions;
	using System.Collections;

	/// <summary>
	/// Est l'objet en charge de garder les références vers nos databases (ici des scriptable object).
	/// </summary>

	public class DatabaseManager : Singleton<DatabaseManager>
	{
		#region Fields
		[SerializeField] private WaveDatabase _waveDatabase = null;
		//[SerializeField] private WeaponDatabase _weaponDatabase = null;
		//[SerializeField] private TowerDatabase _towerDatabase = null;
		#endregion Fields

		#region Properties

		public WaveDatabase WaveDatabase
		{
			get
			{
				return _waveDatabase;
			}
		}

		#endregion Properties

		#region Methods
		protected override void Awake()
		{
			base.Awake();
			Assert.IsNotNull(_waveDatabase);
		}
		#endregion Methods

	}
}
