using System;
using UnityEngine;

namespace Genkit
{
	// Token: 0x02000393 RID: 915
	public static class ColorExtensionMethods
	{
		// Token: 0x06002284 RID: 8836 RVA: 0x000AE31C File Offset: 0x000AC51C
		public static bool Equals(this Color32 x, Color32 y)
		{
			return x.a == y.a && x.b == y.b && x.g == y.g && x.r == y.r;
		}
	}
}
