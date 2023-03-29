using System.Collections.Generic;
using UnityEngine.EventSystems;
using Runtime.Utilities;
using Runtime.Units;
using UnityEngine;

namespace Runtime.Controllers
{
	public class SelectionController : MonoBehaviour
	{
		#region Variables
		[SerializeField] private List<Unit> _selectedUnits = new List<Unit>();
		[SerializeField] private Transform _selectionArea;

		private Vector3 _startOfArea;
		private Vector3 _endOfArea;
		private Vector3 _centerOfArea;
		private Vector3 _sizeOfArea;

		private bool _isAreaSelection;
		#endregion

		#region Methods
		private void AreaSelection()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				_isAreaSelection = false;
				return;
			}

			if (Input.GetMouseButtonDown(0))
			{
				_startOfArea = SelectionUtility.MouseToTerrainPosition();
				_endOfArea = _startOfArea;
			}
			else if (Input.GetMouseButton(0))
			{
				_endOfArea = SelectionUtility.MouseToTerrainPosition();

				if (Vector3.Distance(_startOfArea, _endOfArea) > 1)
				{
					_selectionArea.gameObject.SetActive(true);
					_isAreaSelection = true;
					_centerOfArea = (_startOfArea + _endOfArea) / 2;
					_sizeOfArea = (_endOfArea - _startOfArea);
					_selectionArea.transform.position = _centerOfArea;
					_selectionArea.transform.localScale = _sizeOfArea + Vector3.up;
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				if (_isAreaSelection)
				{
					SelectUnitsInArea();
					_isAreaSelection = false;
					_selectionArea.gameObject.SetActive(false);
				}
				else
				{
					SelectOneUnit();
				}
			}

			if(Input.GetMouseButtonUp(1))
			{
				if (_selectedUnits.Count == 0)
				{
					return;
				}
				else
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					Physics.Raycast(ray, out RaycastHit hit);

					foreach (Unit unit in _selectedUnits)
					{
						unit.MoveToTargetPosition(SelectionUtility.MouseToTerrainPosition());
					}
				}
			}
		}

		private void SelectOneUnit()
		{
			DeselectUnits();

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			Physics.Raycast(ray, out RaycastHit hit);

			if (hit.collider.TryGetComponent(out Unit unit))
			{
				_selectedUnits.Add(unit);
				unit.Selected();
			}
		}

		private void SelectUnitsInArea()
		{
			DeselectUnits();

			_sizeOfArea.Set(Mathf.Abs(_sizeOfArea.x / 2), 1, Mathf.Abs(_sizeOfArea.z / 2));
			RaycastHit[] hits = Physics.BoxCastAll(_centerOfArea, _sizeOfArea, Vector3.up);

			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.TryGetComponent(out Unit unit))
				{
					_selectedUnits.Add(unit);
					unit.Selected();
				}
			}
		}

		private void DeselectUnits()
		{
			foreach (Unit unit in _selectedUnits)
			{
				unit.Deselected();
			}

			_selectedUnits.Clear();
		}
		#endregion

		#region MonoBehaviour Callbacks
		private void Start()
		{
			_selectionArea.gameObject.SetActive(false);
			_selectedUnits = new List<Unit>();
		}

		private void Update()
		{
			AreaSelection();
		}
		#endregion
	}
}
