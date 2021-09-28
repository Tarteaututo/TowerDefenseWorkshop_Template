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

		[SerializeField]
		private Color _lineColor = Color.white;

		private readonly Vector3 _offset = new Vector3(0, 0.5f, 0);

		//public List<Transform> Waypoints { get => _waypoints; }
		public List<Transform> Waypoints
		{
			get
			{
				return _waypoints;
			}
		}

		public Transform FirstWaypoint
		{
			get
			{
				if (_waypoints != null && _waypoints.Count > 1)
				{
					return _waypoints[0];
				}
				else
				{
					return null;
				}
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
					var color = Handles.color;
					Handles.color = _lineColor;
					{
						Handles.DrawLine(currentWaypoint.position + _offset, nextWaypoint.position + _offset);
					}
					Handles.color = color;
				}
			}
		}
	}
}