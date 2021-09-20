namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class WaveManager : MonoBehaviour
	{
		[SerializeField]
		private WaveDatabase _waveDatabase = null;

		private void UpdateWave()
		{
			// TODO  a WaveDatabase.GetWave etc
			//var entity = _waves[_currentWaveIndex].TryGetNextEntity();
		}
	}
}