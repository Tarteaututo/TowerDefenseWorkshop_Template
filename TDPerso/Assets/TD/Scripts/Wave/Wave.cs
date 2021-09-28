namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[System.Serializable]
	public class Wave
	{
		[SerializeField]
		private List<WaveEntityDescription> _waveEntitiesDescriptions = null;

		[SerializeField]
		private float _durationBetweenSpawnedEntity = 1f;

		[System.NonSerialized]
		private Queue<WaveEntityDescription> _waveElements;

		public List<WaveEntityDescription> WaveEntitiesDescription
		{
			get
			{
				return _waveEntitiesDescriptions;
			}
		}

		public float DurationBetweenSpawnedEntity
		{
			get
			{
				return _durationBetweenSpawnedEntity;
			}
		}

		public bool HasWaveElementsLeft
		{
			get
			{
				return _waveElements != null && _waveElements.Count != 0;
			}
		}

		public Wave(Wave other)
		{
			_waveEntitiesDescriptions = other._waveEntitiesDescriptions;
			_durationBetweenSpawnedEntity = other._durationBetweenSpawnedEntity;
			InitializeRuntime();
		}

		public WaveEntityDescription PeekNextWaveElement()
		{
			return _waveElements.Count != 0 ? _waveElements.Peek() : null;
		}

		public WaveEntityDescription GetNextWaveElement()
		{
			return _waveElements.Count != 0 ? _waveElements.Dequeue() : null;
		}

		private void InitializeRuntime()
		{
			_waveElements = new Queue<WaveEntityDescription>();
			for (int i = 0, length = _waveEntitiesDescriptions.Count; i < length; i++)
			{
				_waveElements.Enqueue(_waveEntitiesDescriptions[i]);
			}
		}

	}
}