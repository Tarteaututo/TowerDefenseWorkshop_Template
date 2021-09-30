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

	public enum SpawnerStatus
	{
		Inactive = 0,
		WaveRunning
	}

	public class SpawnerManager : MonoBehaviour
	{
		[SerializeField]
		private List<EntitySpawner> _spawners = null;

		[SerializeField]
		private bool _autoStartNextWaves = false;

		[System.NonSerialized]
		private int _currentWaveSetIndex = -1;

		[System.NonSerialized]
		private int _currentWaveRunning = 0;

		public delegate void SpawnerEvent(SpawnerManager sender, SpawnerStatus status);
		public event SpawnerEvent WaveStatusChanged = null;

		[ContextMenu("Start waves")]
		public void StartWaves()
		{
			// Start a new wave set only if there are no currently a wave running
			if (_currentWaveRunning <= 0)
			{
				StartNewWaveSet();
			}
		}

		public void StartNewWaveSet()
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
						Debug.LogWarningFormat("{0}.StartNewWaveSet() There are more wave ({1}) than available spawner ({2})", GetType().Name, waves.Count, i);
						break;
					}
					if (waves[i] == null)
					{
						Debug.LogWarningFormat("{0}.StartNewWaveSet() Null reference found in WaveSet at index {1}, ignoring.", GetType().Name, i);
						break;
					}
					_currentWaveRunning += 1;
					var spawner = _spawners[i];
					spawner.StartWave(waves[i]);
					spawner.WaveEnded -= Spawner_OnWaveEnded;
					spawner.WaveEnded += Spawner_OnWaveEnded;

					WaveStatusChanged?.Invoke(this, SpawnerStatus.WaveRunning);
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

			WaveStatusChanged?.Invoke(this, SpawnerStatus.Inactive);

			// should we run a new wave?
			if (_autoStartNextWaves == true && _currentWaveRunning <= 0)
			{
				StartNewWaveSet();
			}
		}
	}
}