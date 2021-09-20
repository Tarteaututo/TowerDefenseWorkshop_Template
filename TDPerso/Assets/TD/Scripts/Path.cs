namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	public class Path : MonoBehaviour
	{
		[SerializeField]
		private List<Transform> _waypoints = null;

		[SerializeField]
		private bool _showGizmos = true;

		private readonly Vector3 _offset = new Vector3(0, 0.5f, 0);

		//public List<Transform> Waypoints { get => _waypoints; }
		public List<Transform> Waypoints
		{
			get
			{
				return _waypoints;
			}
		}

		private void OnDrawGizmos()
		{
			if (_showGizmos == false || _waypoints == null)
			{
				return;
			}

			for (int i = 0, length = _waypoints.Count - 1; i < length; i++)
			{
				Transform currentWaypoint = _waypoints[i];
				Transform nextWaypoint = _waypoints[i + 1];
				if (currentWaypoint != null && nextWaypoint != null)
				{
					Handles.DrawLine(currentWaypoint.position + _offset, nextWaypoint.position + _offset);
				}
			}
		}
	}
}