using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x020004A8 RID: 1192
	public class ControlId : MonoBehaviour
	{
		// Token: 0x06002DA7 RID: 11687 RVA: 0x000F9904 File Offset: 0x000F7B04
		private static bool CanvasEnabled(GameObject go)
		{
			if (go == null)
			{
				return true;
			}
			Canvas canvas = ((go != null) ? go.GetComponent<Canvas>() : null);
			if (canvas != null && !canvas.enabled)
			{
				return false;
			}
			GameObject gameObject;
			if (go == null)
			{
				gameObject = null;
			}
			else
			{
				Transform transform = go.transform;
				if (transform == null)
				{
					gameObject = null;
				}
				else
				{
					Transform parent = transform.parent;
					gameObject = ((parent != null) ? parent.gameObject : null);
				}
			}
			return ControlId.CanvasEnabled(gameObject);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000F9968 File Offset: 0x000F7B68
		public static GameObject Get(string id, bool includeInactive = false)
		{
			ControlId controlId = global::UnityEngine.Object.FindObjectsByType<ControlId>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None).FirstOrDefault<ControlId>((ControlId o) => o.id == id && ControlId.CanvasEnabled(o.gameObject));
			if (controlId == null)
			{
				return null;
			}
			return controlId.gameObject;
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000F99AC File Offset: 0x000F7BAC
		public static IEnumerable<GameObject> GetAll(string id, bool includeInactive = false)
		{
			return from o in global::UnityEngine.Object.FindObjectsByType<ControlId>(includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None)
				where ControlId.CanvasEnabled(o.gameObject)
				select o into cid
				select cid.gameObject;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000F9A10 File Offset: 0x000F7C10
		public static void Assign(GameObject go, string id, bool findNestedComponent = false)
		{
			if (go == null)
			{
				MetricsManager.LogWarning("ControlID::Assign called with null go");
				return;
			}
			ControlId controlId = go.GetComponent<ControlId>();
			if (controlId == null && findNestedComponent)
			{
				controlId = go.GetComponentInChildren<ControlId>();
			}
			if (controlId == null)
			{
				controlId = go.AddComponent<ControlId>();
			}
			controlId.id = id;
		}

		// Token: 0x040019E6 RID: 6630
		public string id;
	}
}
