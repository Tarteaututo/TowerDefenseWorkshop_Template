namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public enum State
	{
		Available = 0,
		GhostVisible
	}

	public class TowerSlotController : MonoBehaviour
	{
		[SerializeField]
		private TowerSlot[] _towerSlots = null;

		[System.NonSerialized]
		private State _state = State.Available;

		[System.NonSerialized]
		private TowerDescription _currentTowerDescription = null;

		private void OnEnable()
		{
			for (int i = 0, length = _towerSlots.Length; i < length; i++)
			{
				_towerSlots[i].OnTowerSlotClicked -= TowerSlotController_OnTowerSlotClicked;
				_towerSlots[i].OnTowerSlotClicked += TowerSlotController_OnTowerSlotClicked;
			}
		}

		private void OnDisable()
		{
			for (int i = 0, length = _towerSlots.Length; i < length; i++)
			{
				_towerSlots[i].OnTowerSlotClicked -= TowerSlotController_OnTowerSlotClicked;
			}
		}

		private void TowerSlotController_OnTowerSlotClicked(TowerSlot sender)
		{
			// Instantiate ghost
			// get picker, feed the ghost
			// 
			_currentTowerDescription = sender.TowerDescription;
			ChangeState(State.GhostVisible);
		}

		private void Update()
		{
			if (_state == State.GhostVisible)
			{
				if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape) == true)
				{
					ChangeState(State.Available);
				}
			}
		}

		public void ChangeState(State newState)
		{
			switch (newState)
			{
				case State.Available:
				{
					var playerPickerController = LevelReferences.Instance.PlayerPickerController;
					playerPickerController.DestroyGhost();
					playerPickerController.Activate(false);
					// destroy ghost
					_currentTowerDescription = null;
				}
				break;
				case State.GhostVisible:
				{
					var playerPickerController = LevelReferences.Instance.PlayerPickerController;
					playerPickerController.ActivateWithGhost(_currentTowerDescription.Instantiate());
				}
					break;
				default:
					break;
			}
			_state = newState;
		}
	}
}
