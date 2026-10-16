using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000039 RID: 57
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[Serializable]
	public class TextColorGradient : ScriptableObject
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000B348 File Offset: 0x00009548
		public TextColorGradient()
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = TextColorGradient.k_DefaultColor;
			this.topRight = TextColorGradient.k_DefaultColor;
			this.bottomLeft = TextColorGradient.k_DefaultColor;
			this.bottomRight = TextColorGradient.k_DefaultColor;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000B397 File Offset: 0x00009597
		public TextColorGradient(Color color)
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000B3CB File Offset: 0x000095CB
		public TextColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorGradientMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x0400015A RID: 346
		public ColorGradientMode colorMode = ColorGradientMode.FourCornersGradient;

		// Token: 0x0400015B RID: 347
		public Color topLeft;

		// Token: 0x0400015C RID: 348
		public Color topRight;

		// Token: 0x0400015D RID: 349
		public Color bottomLeft;

		// Token: 0x0400015E RID: 350
		public Color bottomRight;

		// Token: 0x0400015F RID: 351
		private const ColorGradientMode k_DefaultColorMode = ColorGradientMode.FourCornersGradient;

		// Token: 0x04000160 RID: 352
		private static readonly Color k_DefaultColor = Color.white;
	}
}
