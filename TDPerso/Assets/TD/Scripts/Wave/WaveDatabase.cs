﻿namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public enum EntityType
	{
		None,
		Light,
		Heavy,
		Speedy
	}

	[System.Serializable]
	public class WaveEntityData
	{
		[SerializeField]
		private WaveEntity _waveEntityPrefab = null;

		[SerializeField]
		private EntityType _entityType = EntityType.None;

		public WaveEntity WaveEntityPrefab => _waveEntityPrefab;
		public EntityType EntityType => _entityType;
	}


	[CreateAssetMenu(menuName = "Gameleon/Database/WaveDatabase")]
	public class WaveDatabase : ScriptableObject
	{
		[SerializeField]
		private List<WaveEntityData> _waveEntityDatas = null;

		[SerializeField]
		private List<WaveSet> _waves = null;

		public List<WaveSet> Waves
		{
			get { return _waves; }
		}

		public bool GetWaveElementFromType(EntityType entityType, out WaveEntity outEntity)
		{
			WaveEntityData waveEntityData = _waveEntityDatas.Find(entity => entity.EntityType == entityType);
			if (waveEntityData != null)
			{
				outEntity = waveEntityData.WaveEntityPrefab;
				return true;
			}
			outEntity = null;
			return false;
		}
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