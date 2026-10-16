using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000074 RID: 116
	public static class TMP_Math
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0002E2EA File Offset: 0x0002C4EA
		public static bool Approximately(float a, float b)
		{
			return b - 0.0001f < a && a < b + 0.0001f;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002E304 File Offset: 0x0002C504
		public static int Mod(int a, int b)
		{
			int num = a % b;
			if (num >= 0)
			{
				return num;
			}
			return num + b;
		}

		// Token: 0x04000504 RID: 1284
		public const float FLOAT_MAX = 32767f;

		// Token: 0x04000505 RID: 1285
		public const float FLOAT_MIN = -32767f;

		// Token: 0x04000506 RID: 1286
		public const int INT_MAX = 2147483647;

		// Token: 0x04000507 RID: 1287
		public const int INT_MIN = -2147483647;

		// Token: 0x04000508 RID: 1288
		public const float FLOAT_UNSET = -32767f;

		// Token: 0x04000509 RID: 1289
		public const int INT_UNSET = -32767;

		// Token: 0x0400050A RID: 1290
		public static Vector2 MAX_16BIT = new Vector2(32767f, 32767f);

		// Token: 0x0400050B RID: 1291
		public static Vector2 MIN_16BIT = new Vector2(-32767f, -32767f);
	}
}
