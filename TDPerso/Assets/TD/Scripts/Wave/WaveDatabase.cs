namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[System.Serializable]
	public class Wave
	{
		[SerializeField]
		private List<WaveEntity> _waveEntities = null;

		[SerializeField]
		private Timer _internalTimer = null;

		[System.NonSerialized]
		private int _currentEntityIndex = 0;

		public bool IsWaveEnded()
		{
			return _currentEntityIndex >= _waveEntities.Count;
		}

		public float GetTotalDuration()
		{
			return _internalTimer.GetDuration() * _waveEntities.Count;
		}

		public void StartWave()
		{
			_internalTimer.Start();
		}

		public WaveEntity TryGetNextEntity()
		{
			bool canGetEntity = _internalTimer.Update();

			if (canGetEntity == true && _waveEntities.Count < _currentEntityIndex)
			{
				_currentEntityIndex += 1;
				if (IsWaveEnded() == false)
				{
					_internalTimer.Start();
				}
				return _waveEntities[_currentEntityIndex - 1];
			}
			return null;
		}
	}

	[CreateAssetMenu(fileName = "Wave Description", menuName = "Gameleon/Wave")]
	public class WaveDatabase : ScriptableObject
	{
		[SerializeField]
		private List<Wave> _waves = null;

		[SerializeField]
		private Timer _timer = null;

		[System.NonSerialized]
		private int _currentWaveIndex = 0;

		public void UpdateWave()
		{
		
		}

		public void GetWave()
		{
		}
	}
}