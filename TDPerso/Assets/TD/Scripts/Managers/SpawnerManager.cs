namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public enum SpawnerIndex
	{
		Spawner00,
		Spawner01,
		Spawner02,
	}

	public class SpawnerManager : Singleton<SpawnerManager>
	{
		[SerializeField]
		private List<EntitySpawner> _spawners = null;

		[System.NonSerialized]
		private int _currentWaveSetIndex = -1;

		[System.NonSerialized]
		private int _currentWaveRunning = 0;

		protected override void Start()
		{
			base.Start();
			//StartNewWaveSet();
		}

		[ContextMenu("Start waves")]

		private void StartNewWaveSet()
		{
			_currentWaveSetIndex += 1;
			var waveDatabase = DatabaseManager.Instance.WaveDatabase;

			if (waveDatabase.Waves.Count > _currentWaveSetIndex)
			{
				WaveSet waveSet = waveDatabase.Waves[_currentWaveSetIndex];
				List<Wave> waves = waveSet.Waves;

				for (int i = 0, length = _spawners.Count; i < length; i++)
				{
					if (waves.Count <= i)
					{
						// There are more wave than available spawner
						break;
					}
					if (waves[i] == null)
					{
						// Null reference found in WaveSet
						break;
					}
					_currentWaveRunning += 1;
					var spawner = _spawners[i];
					spawner.StartWave(waves[i]);
					spawner.WaveEnded -= Spawner_OnWaveEnded;
					spawner.WaveEnded += Spawner_OnWaveEnded;
				}
			}
			else
			{
				// No waves left : end game
			}
		}

		private void Spawner_OnWaveEnded(EntitySpawner entitySpawner, Wave wave)
		{
			entitySpawner.WaveEnded -= Spawner_OnWaveEnded;

			_currentWaveRunning -= 1;

			// should we run a new wave?
			if (_currentWaveRunning <= 0)
			{
				StartNewWaveSet();
			}
		}
	}
}