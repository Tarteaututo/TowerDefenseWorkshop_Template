namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using TMPro;
	using UnityEngine;

	public class WeaponController : MonoBehaviour
	{
		[SerializeField]
		private AWeapon _weapon = null;

		public void LookAt(Vector3 position)
		{
			var direction = (position - transform.position).normalized;
			transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
		}

		public void Fire()
		{
			_weapon.Fire();
		}

	}
}