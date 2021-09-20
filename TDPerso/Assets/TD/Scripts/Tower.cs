using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Facade for Tower subsystems
/// </summary>
public class Tower : MonoBehaviour
{
	[SerializeField]
	private WeaponController _weaponController = null;

	[SerializeField]
    private DamageableDetector _damageableDetector = null;

	private void Update()
	{
		if (_damageableDetector.HasAnyDamageableInRange() == true)
		{
			var damageableTarget = _damageableDetector.GetNearestDamageable();
			_weaponController.LookAt(damageableTarget.GetAimPosition());
			_weaponController.Fire();
		}
	}
}
