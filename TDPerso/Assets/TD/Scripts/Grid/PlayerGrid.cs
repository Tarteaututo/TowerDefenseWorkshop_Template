using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrid : MonoBehaviour
{
	[SerializeField]
	private GridPicker _gridPicker = null;

	[System.NonSerialized]
	private bool _isActive = false;

	[ContextMenu("Activate")]
	private void DoActivate() => Activate(true);

	[ContextMenu("Deactivate")]
	private void DoDeactivate() => Activate(false);

	public void Activate(bool isActive)
	{
		_isActive = isActive;
		_gridPicker.Activate(isActive);
	}

	private void Update()
	{
		if (_isActive == true)
		{
			if (_gridPicker.TryGetCell(out Cell cell) == true)
			{

			}
		}
	}
}
