namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class LevelReferences : Singleton<LevelReferences>
	{
		[SerializeField]
		private PlayerPickerController _playerPickerController = null;

		public PlayerPickerController PlayerPickerController => _playerPickerController;
	}
}