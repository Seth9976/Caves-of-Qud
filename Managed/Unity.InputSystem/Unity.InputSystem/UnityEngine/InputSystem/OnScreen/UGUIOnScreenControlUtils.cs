using System;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000098 RID: 152
	internal static class UGUIOnScreenControlUtils
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x000408B8 File Offset: 0x0003EAB8
		public static RectTransform GetCanvasRectTransform(Transform transform)
		{
			if (!(transform.parent != null))
			{
				return null;
			}
			return transform.parent.GetComponentInParent<RectTransform>();
		}
	}
}
