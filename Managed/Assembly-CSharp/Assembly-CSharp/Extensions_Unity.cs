using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public static class Extensions_Unity
{
	// Token: 0x06000E5D RID: 3677 RVA: 0x0005C500 File Offset: 0x0005A700
	public static bool FromHex(this Color c1, string hex)
	{
		Color color;
		return ColorUtility.TryParseHtmlString(hex, out color);
	}
}
