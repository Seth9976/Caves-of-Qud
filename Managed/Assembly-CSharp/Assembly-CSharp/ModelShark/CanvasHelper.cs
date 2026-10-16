using System;
using UnityEngine;

namespace ModelShark
{
	// Token: 0x02000199 RID: 409
	public static class CanvasHelper
	{
		// Token: 0x06000F84 RID: 3972 RVA: 0x00062480 File Offset: 0x00060680
		public static Canvas GetRootCanvas()
		{
			Canvas[] array = global::UnityEngine.Object.FindObjectsByType<Canvas>(FindObjectsSortMode.InstanceID);
			if (array.Length == 0)
			{
				Debug.LogError("No canvas found in scene.");
				return null;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].isRootCanvas)
				{
					return array[i];
				}
			}
			Debug.LogError("No canvas found at the root level of the scene.");
			return null;
		}
	}
}
