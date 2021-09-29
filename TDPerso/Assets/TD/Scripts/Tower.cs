using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Facade for Tower subsystems
/// </summary>
public class Tower : MonoBehaviour, IPickerGhost
{
	[SerializeField]
	private WeaponController _weaponController = null;

	[SerializeField]
    private DamageableDetector _damageableDetector = null;

	private void Awake()
	{
		enabled = false;
	}

	public void Enable(bool isEnabled)
	{
		enabled = isEnabled;
	}

	private void Update()
	{
		if (_damageableDetector.HasAnyDamageableInRange() == true)
		{
			var damageableTarget = _damageableDetector.GetNearestDamageable();
			_weaponController.LookAt(damageableTarget.GetAimPosition());
			_weaponController.Fire();
		}
	}

	Transform IPickerGhost.GetTransform()
	{
		return transform;
	}
}
