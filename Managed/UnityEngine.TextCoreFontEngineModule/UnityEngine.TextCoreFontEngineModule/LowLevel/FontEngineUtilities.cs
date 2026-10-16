using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000F RID: 15
	internal struct FontEngineUtilities
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000058AC File Offset: 0x00003AAC
		internal static bool Approximately(float a, float b)
		{
			return Mathf.Abs(a - b) < 0.001f;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000058D0 File Offset: 0x00003AD0
		internal static int MaxValue(int a, int b, int c)
		{
			return (a < b) ? ((b < c) ? c : b) : ((a < c) ? c : a);
		}
	}
}
