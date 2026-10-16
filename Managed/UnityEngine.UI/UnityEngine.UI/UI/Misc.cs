using System;

namespace UnityEngine.UI
{
	// Token: 0x0200002E RID: 46
	internal static class Misc
	{
		// Token: 0x06000307 RID: 775 RVA: 0x000100E6 File Offset: 0x0000E2E6
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						(obj as GameObject).transform.parent = null;
					}
					Object.Destroy(obj);
					return;
				}
				Object.DestroyImmediate(obj);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001011E File Offset: 0x0000E31E
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
					return;
				}
				Object.Destroy(obj);
			}
		}
	}
}
