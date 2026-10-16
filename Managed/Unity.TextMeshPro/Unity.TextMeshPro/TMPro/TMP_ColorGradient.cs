using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200001B RID: 27
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_ColorGradient : ScriptableObject
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public TMP_ColorGradient()
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = TMP_ColorGradient.k_DefaultColor;
			this.topRight = TMP_ColorGradient.k_DefaultColor;
			this.bottomLeft = TMP_ColorGradient.k_DefaultColor;
			this.bottomRight = TMP_ColorGradient.k_DefaultColor;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002D15 File Offset: 0x00000F15
		public TMP_ColorGradient(Color color)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002D47 File Offset: 0x00000F47
		public TMP_ColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x0400006E RID: 110
		public ColorMode colorMode = ColorMode.FourCornersGradient;

		// Token: 0x0400006F RID: 111
		public Color topLeft;

		// Token: 0x04000070 RID: 112
		public Color topRight;

		// Token: 0x04000071 RID: 113
		public Color bottomLeft;

		// Token: 0x04000072 RID: 114
		public Color bottomRight;

		// Token: 0x04000073 RID: 115
		private const ColorMode k_DefaultColorMode = ColorMode.FourCornersGradient;

		// Token: 0x04000074 RID: 116
		private static readonly Color k_DefaultColor = Color.white;
	}
}
