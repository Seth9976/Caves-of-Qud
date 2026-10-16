using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E9 RID: 489
	internal class DisposeHelper
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00043108 File Offset: 0x00041308
		[Conditional("UNITY_UIELEMENTS_DEBUG_DISPOSE")]
		public static void NotifyMissingDispose(IDisposable disposable)
		{
			bool flag = disposable == null;
			if (!flag)
			{
				Debug.LogError("An IDisposable instance of type '" + disposable.GetType().FullName + "' has not been disposed.");
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00043140 File Offset: 0x00041340
		public static void NotifyDisposedUsed(IDisposable disposable)
		{
			Debug.LogError("An instance of type '" + disposable.GetType().FullName + "' is being used although it has been disposed.");
		}
	}
}
