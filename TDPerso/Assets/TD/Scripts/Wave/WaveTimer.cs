//namespace GSGD1
//{
//	using System.Collections;
//	using System.Collections.Generic;
//	using UnityEngine;

//	//TODO: events when wave and wavelement are popped and when the wave is ended
//	//TODO REGRESSION : handle waiting time added
//	public class WaveTimer : Timer
//	{
//		private Queue<Wave> _waves = new Queue<Wave>();
//		private Path _path;
//		private RuntimeWave _currentWave;
//		private int _currentWaveWaitingTime = -1;

//		public bool IsAllWavesEnded
//		{
//			get
//			{
//				return
//					_currentWave.HasWaveElements == false &&
//					_waves.Count != 0;
//			}
//		}

//		public WaveTimer(
//			List<Wave> waves,
//			Path path)
//			: base()
//		{
//			for (int i = 0, length = waves.Count; i < length; i++)
//			{
//				_waves.Enqueue(waves[i]);
//			}
//			_path = path;
//		}
		
//		protected override void OnPreStart()
//		{
//			base.OnPreStart();
//			if (_currentWave.HasWaveElements == false)
//			{
//				TryInitializeWaveIfAny();
//				Set(_currentWaveWaitingTime, false);
//			}
//		}

//		protected override void OnUpdateTimerReached()
//		{
//			base.OnUpdateTimerReached();
//			WaveEntityDescription nextWaveElement = _currentWave.GetNextWaveElement();
//			if (nextWaveElement != null)
//			{
//				if (DatabaseManager.Instance.WaveDatabase.GetWaveElementFromType(nextWaveElement.EntityType, out WaveEntity waveEntity) == true)
//				{
//					//waveEntity = EntitySpawner.Instance.InstantiateEntity(waveEntity);
//					waveEntity.SetPath(_path);
//				}
//			}
//			if (_currentWave.PeekNextWaveElement() == null)
//			{
//				TryInitializeWaveIfAny();
//			}
//		}

//		private void TryInitializeWaveIfAny()
//		{
//			if (_waves != null && _waves.Count != 0)
//			{
//				Wave wave = _waves.Dequeue();
//				_currentWave = new RuntimeWave(wave.Actors);
//				_currentWaveWaitingTime = wave.WaitingDuration;
//			}
//			else
//			{
//				Stop(false);
//			}
//		}

//	}
//}
