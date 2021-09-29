using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickerGhost
{
	Transform GetTransform();
}

public class PlayerPickerController : MonoBehaviour
{
	[SerializeField]
	private GridPicker _gridPicker = null;

	[System.NonSerialized]
	private Transform _ghost = null;

	[System.NonSerialized]
	private bool _isActive = false;

	[ContextMenu("Activate")]
	private void DoActivate() => Activate(true);

	[ContextMenu("Deactivate")]
	private void DoDeactivate() => Activate(false);

	public void Activate(bool isActive)
	{
		_isActive = isActive;
		_gridPicker.Activate(isActive, true);
	}

	//TODO AL : ui to call setghost, etc...
	public void ActivateWithGhost(IPickerGhost ghost)
	{
		_ghost = ghost.GetTransform();
		Activate(true);
	}

	public void DestroyGhost()
	{
		Destroy(_ghost.gameObject);
	}

	private void Update()
	{
		if (_isActive == true)
		{
			if (_gridPicker.TryGetCell(out Cell cell) == true)
			{
				_ghost.transform.position = _gridPicker.CellPosition;
			}
			else if (_ghost != null)
			{
				_ghost.transform.position = _gridPicker.HitPosition;
			}
		}

	}
}
