namespace GSGD1
{
	using System.Collections;
	using UnityEditor;
	using UnityEngine;
	public class GridPicker : MonoBehaviour
	{
		[SerializeField] private Transform _highlighter = null;
		[SerializeField] private GridBehaviour _grid = null;
		[SerializeField] private LayerMask _layerMask;

		private Vector3 _hitPosition = Vector3.zero;
		private Transform _computedGridPosition = null;

		public Transform GetGridPosition
		{
			get
			{
				if (enabled == true && _computedGridPosition == null)
				{
					_computedGridPosition = ComputeGridPosition();
				}
				return _computedGridPosition;
			}
		}

		public Vector3 HitPosition
		{
			get
			{
				return _hitPosition;
			}
		}

		private void OnDestroy()
		{
			_computedGridPosition = null;
		}

		private void LateUpdate()
		{
			_computedGridPosition = null;
		}

		public bool TryGetCell(out Cell cell)
		{
			Transform hitTransform = GetGridPosition;
			if (hitTransform != null)
			{
				Debug.Log(hitTransform.name);
				cell = hitTransform.GetComponentInParent<Cell>();
				return cell != null;
			}
			return cell = null;
		}
		public void Activate(bool isActive)
		{
			enabled = isActive;
			if (_highlighter != null)
			{
				_highlighter.gameObject.SetActive(isActive);
			}
			_computedGridPosition = null;
		}

		private Transform ComputeGridPosition()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit/*, _layerMask*/))
			{
				Debug.Log("OUI");
				//Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//if (Physics.Raycast(mousePos, Camera.main.transform.forward, out hit, float.MaxValue, _layerMask))
			//{
				if (_highlighter != null)
				{
					Vector3 highlightPosition = _grid.GetCellCenter(hit.point);
					highlightPosition.y = 0.1f;
					_highlighter.transform.position = highlightPosition;
					_highlighter.gameObject.SetActive(true);
				}
				_hitPosition = hit.point;
				return hit.transform;
			}
			else
			{
				if (_highlighter != null)
				{
					_highlighter.gameObject.SetActive(false);
				}
				//Debug.Log("found nothing");
				return null;
			}
		}

		private void OnDrawGizmos()
		{
			var color = Handles.color;
			Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
			Handles.color = Color.blue;
			{
				Camera cam = Camera.main;

				//Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
				//Vector3 mousePos = cam.ViewportToWorldPoint(Input.mousePosition);
				//Handles.DrawLine(mousePos, mousePos + cam.transform.forward * 100);

				//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				//Ray ray = cam.ViewportPointToRay(Input.mousePosition);

				Handles.DrawLine(ray.origin, ray.origin + (ray.direction * 100));
			}

			Handles.color = color;
		}

		void Update()
		{
			Camera cam = Camera.main;

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;

			//if (Physics.Raycast(ray.origin, ray.direction, out hit))
			if (Physics.Raycast(ray, out hit))
			{
				if (Input.GetMouseButtonDown(0))
				{
					Debug.Log(hit.collider.gameObject);
				}
			}
		}

	}
}
