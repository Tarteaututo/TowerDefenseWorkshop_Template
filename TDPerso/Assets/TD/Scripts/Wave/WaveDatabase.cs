﻿namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
#endif //UNITY_EDITOR

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

#if UNITY_EDITOR
	[CustomEditor(typeof(WaveDatabase))]
	public class WaveEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			serializedObject.Update();
			EditorGUILayout.Space(24);

			var waveDatabase = (serializedObject.targetObject as WaveDatabase);
			EditorGUILayout.TextArea(string.Format("Total Duration : {0} seconds", GetWaveDuration(waveDatabase.Waves).ToString()));
		}

		private float GetWaveDuration(List<WaveSet> waves)
		{
			float duration = 0;
			for (int i = 0, length = waves.Count; i < length; i++)
			{
				duration += waves[i].GetWaveDuration();
			}
			return duration;
		}
	}
#endif //UNITY_EDITOR

}