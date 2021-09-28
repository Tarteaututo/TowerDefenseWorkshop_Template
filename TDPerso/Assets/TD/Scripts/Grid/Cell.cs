namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Cell : MonoBehaviour
	{
		#region Fields
		#region Private
		private Tower _towerChild = null;
		#endregion Private
		#endregion Fields

		#region Properties
		public bool HasChild
		{
			get
			{
				return _towerChild != null;
			}
		}
		#endregion Properties

		#region Methods
		#region Public
		public void SetChild(Tower tower)
		{
			tower.transform.SetParent(transform);
			tower.transform.localPosition = Vector3.zero;
			_towerChild = tower;
		}
		#endregion Public
		#endregion Methods
	}
}