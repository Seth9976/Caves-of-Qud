using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000018 RID: 24
	internal static class ColorUtilities
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003FA4 File Offset: 0x000021A4
		internal static bool CompareColors(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003FF4 File Offset: 0x000021F4
		internal static Color32 MultiplyColors(Color32 c1, Color32 c2)
		{
			byte b = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte b2 = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b3 = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte b4 = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(b, b2, b3, b4);
		}
	}
}
