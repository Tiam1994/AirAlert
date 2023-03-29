using UnityEngine;

namespace Runtime.Utilities
{
	public class SelectionUtility : MonoBehaviour
	{
		public static Vector3 MouseToTerrainPosition()
		{
			Vector3 position = Vector3.zero;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, LayerMask.GetMask("Ground")))
			{
				position = info.point;
			}

			return position;
		}

		public static RaycastHit CameraRay()
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100))
			{
				return info;
			}

			return new RaycastHit();
		}
	}
}
