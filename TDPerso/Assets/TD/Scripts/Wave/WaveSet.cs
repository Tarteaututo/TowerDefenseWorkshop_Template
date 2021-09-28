namespace GSGD1
{
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Gameleon/Database/WaveSet")]
	public class WaveSet : ScriptableObject
	{
		[Tooltip("Index is the spawner index. _waves[0] will be spawner00, _wave[1] spawner01, etc...")]
		[SerializeField]
		private List<Wave> _waves = null;

		[SerializeField]
		private float _waitingDurationBefore = 0f;

		[SerializeField]
		private float _waitingDurationAfter = 5f;

		public List<Wave> Waves => _waves;

		public float WaitingDurationBefore => _waitingDurationBefore; 
		public float WaitingDurationAfter => _waitingDurationAfter;
	}
}




//	using System.Collections;
//	using System.Collections.Generic;
//	using UnityEngine;

//	[System.Serializable]
//	public class Wave
//	{
//		[SerializeField]
//		private List<WaveEntity> _waveEntities = null;

//		[SerializeField]
//		private Timer _internalTimer = null;

//		[System.NonSerialized]
//		private int _currentEntityIndex = 0;

//		public bool IsWaveEnded()
//		{
//			return _currentEntityIndex >= _waveEntities.Count;
//		}

//		public float GetTotalDuration()
//		{
//			return _internalTimer.GetDuration() * _waveEntities.Count;
//		}

//		public void StartWave()
//		{
//			_internalTimer.Start();
//		}

//		public void ResetWave()
//		{
//			_internalTimer.Stop();
//			_currentEntityIndex = 0;
//		}

//		public WaveEntity TryGetNextEntity()
//		{
//			bool canGetEntity = _internalTimer.Update();

//			if (canGetEntity == true && _waveEntities.Count < _currentEntityIndex)
//			{
//				_currentEntityIndex += 1;
//				if (IsWaveEnded() == false)
//				{
//					_internalTimer.Start();
//				}
//				return _waveEntities[_currentEntityIndex - 1];
//			}
//			return null;
//		}
//	}

//	[CreateAssetMenu(fileName = "Wave", menuName = "Gameleon/Wave")]
//	public class WaveDatabase : ScriptableObject
//	{
//		[SerializeField]
//		private List<Wave> _waves = null;

//		[SerializeField]
//		private Timer _timer = null;

//		[System.NonSerialized]
//		private Wave _currentWave = null;

//		[System.NonSerialized]
//		private int _currentWaveIndex = 0;

//		public void TryGetNextEntityFromWave()
//		{
//			if (_currentWave.IsWaveEnded() && _currentWaveIndex < _waves.Count + 1)
//			{
//				_currentWaveIndex += 1;
//				_currentWave = _waves[_currentWaveIndex];
//			}
//			_currentWave.TryGetNextEntity();
//		}

//		public void GetWave()
//		{
//		}
//	}
//}