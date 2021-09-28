namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Assertions;



	public class EntitySpawner : MonoBehaviour
	{
		[SerializeField]
		private Transform _instancesRoot = null;

		[SerializeField]
		private Path _path = null;

		[System.NonSerialized]
		private Timer _timer = new Timer();

		[System.NonSerialized]
		private Wave _wave = null;

		[System.NonSerialized]
		private List<WaveEntity> _runtimeWaveEntities = new List<WaveEntity>();

		public event System.Action<EntitySpawner, Wave> WaveStarted = null;
		public event System.Action<EntitySpawner, Wave> WaveEnded = null;
		public event System.Action<EntitySpawner, WaveEntity> EntitySpawned = null;
		public event System.Action<EntitySpawner, WaveEntity> EntityDestroyed = null;

		public void StartWave(Wave wave)
		{
			_wave = new Wave(wave);
			_timer.Set(wave.DurationBetweenSpawnedEntity).Start();
			WaveStarted?.Invoke(this, wave);
		}

		private WaveEntity InstantiateEntity(WaveEntity entityPrefab)
		{
			WaveEntity entityInstance = Instantiate(entityPrefab, _instancesRoot);
			_runtimeWaveEntities.Add(entityInstance);
			EntitySpawned?.Invoke(this, entityInstance);
			return entityInstance;
		}

		private void Update() => UpdateWave();

		private void UpdateWave()
		{
			if (_timer != null)
			{
				bool shouldInstantiateEntity = _timer.Update();

				if (shouldInstantiateEntity == true)
				{
					if (_wave.HasWaveElementsLeft == true)
					{
						var nextEntity = _wave.GetNextWaveElement();

						if (DatabaseManager.Instance.WaveDatabase.GetWaveElementFromType(nextEntity.EntityType, out WaveEntity outEntity) == true)
						{
							outEntity = InstantiateEntity(outEntity);
							outEntity.SetPath(_path);
							_timer.Set(_wave.DurationBetweenSpawnedEntity + nextEntity.ExtraDurationAfterSpawned).Start();
						}
						else
						{
							Debug.LogErrorFormat("{0}.UpdateWave() cannot GetWaveElementFromType {1}, no corresponding type found in database.", GetType().Name, nextEntity.EntityType);
							return;
						}
					}
					else
					{
						WaveEnded?.Invoke(this, _wave);
					}
				}
			}
		}
	}

	///// <summary>
	///// L'utilité de cette class est dans le noms des méthodes publique : elle crée les entities, les garde en mémoire, les update puis les détruits.
	///// </summary>
	//public class EntitySpawner : MonoBehaviour
	//{
	//	[SerializeField]
	//	private Transform _instancesRoot = null;

	//	[SerializeField]
	//	private Path _path = null;

	//	private List<WaveEntity> _runtimeWaveEntities = new List<WaveEntity>();
	//	private Stack<WaveEntity> _waveEntitiesToDestroy = new Stack<WaveEntity>();

	//	[System.NonSerialized]
	//	private WaveTimer _waveTimer = null;

	//	public Transform InstanceRoot
	//	{
	//		get { return _instancesRoot; }
	//	}

	//	public Stack<WaveEntity> WaveEntitiesToDestroy
	//	{
	//		get { return _waveEntitiesToDestroy; }
	//	}

	//	public delegate void OnEntitySpawned(WaveEntity entity);
	//	public delegate void OnEntityDestroyed(WaveEntity entity);

	//	private OnEntitySpawned _entitySpawned = null;
	//	public event OnEntitySpawned EntitySpawned
	//	{
	//		add
	//		{
	//			_entitySpawned -= value;
	//			_entitySpawned += value;
	//		}
	//		remove
	//		{
	//			_entitySpawned -= value;
	//		}
	//	}

	//	private OnEntityDestroyed _entityDestroyed = null;
	//	public event OnEntityDestroyed EntityDestroyed
	//	{
	//		add
	//		{
	//			_entityDestroyed -= value;
	//			_entityDestroyed += value;
	//		}
	//		remove
	//		{
	//			_entityDestroyed -= value;
	//		}
	//	}

	//	[ContextMenu("Start wave")]
	//	public void StartWave()
	//	{
	//		var waveDatabase = DatabaseManager.Instance.WaveDatabase;
	//		_waveTimer = new WaveTimer(waveDatabase.Waves, _path);
	//		_waveTimer.Start();
	//	}

	//	public WaveEntity InstantiateEntity(WaveEntity entityPrefab)
	//	{
	//		WaveEntity entityInstance = Instantiate(entityPrefab, _instancesRoot);
	//		_runtimeWaveEntities.Add(entityInstance);
	//		if (_entitySpawned != null)
	//		{
	//			_entitySpawned(entityInstance);
	//		}
	//		return entityInstance;
	//	}

	//	public void DestroyEntity(WaveEntity entityInstance)
	//	{
	//		if (_runtimeWaveEntities.Contains(entityInstance) == true)
	//		{
	//			_runtimeWaveEntities.Remove(entityInstance);
	//			if (_entityDestroyed != null)
	//			{
	//				_entityDestroyed(entityInstance);
	//			}
	//			Destroy(entityInstance.gameObject);
	//		}
	//	}

	//	public void UpdateEntities(float deltaTime)
	//	{
	//		if (_runtimeWaveEntities == null || _runtimeWaveEntities.Count == 0)
	//		{
	//			return;
	//		}
	//		for (int i = 0, length = _runtimeWaveEntities.Count; i < length; i++)
	//		{
	//			_runtimeWaveEntities[i].UpdateEntity(deltaTime);
	//		}
	//		for (int i = 0, length = _waveEntitiesToDestroy.Count; i < length; i++)
	//		{
	//			DestroyEntity(_waveEntitiesToDestroy.Pop());
	//		}
	//	}

	//	private void Awake()
	//	{
	//		Assert.IsNotNull(_instancesRoot);
	//	}

	//	private void OnDestroy()
	//	{
	//		if (_runtimeWaveEntities != null)
	//		{
	//			_runtimeWaveEntities.Clear();
	//			_runtimeWaveEntities = null;
	//		}
	//	}

	//	private void Update()
	//	{
	//		if (_waveTimer != null)
	//		{
	//			_waveTimer.Update();
	//			UpdateEntities(Time.deltaTime);
	//		}
	//	}
	//}
}