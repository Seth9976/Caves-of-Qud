using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200067A RID: 1658
	internal struct TextCoreSettings : IEquatable<TextCoreSettings>
	{
		// Token: 0x06003053 RID: 12371 RVA: 0x000B6E88 File Offset: 0x000B5088
		public override bool Equals(object obj)
		{
			return obj is TextCoreSettings && this.Equals((TextCoreSettings)obj);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000B6EB4 File Offset: 0x000B50B4
		public bool Equals(TextCoreSettings other)
		{
			return other.faceColor == this.faceColor && other.outlineColor == this.outlineColor && other.outlineWidth == this.outlineWidth && other.underlayColor == this.underlayColor && other.underlayOffset == this.underlayOffset && other.underlaySoftness == this.underlaySoftness;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000B6F34 File Offset: 0x000B5134
		public override int GetHashCode()
		{
			int num = 75905159;
			num = num * -1521134295 + this.faceColor.GetHashCode();
			num = num * -1521134295 + this.outlineColor.GetHashCode();
			num = num * -1521134295 + this.outlineWidth.GetHashCode();
			num = num * -1521134295 + this.underlayColor.GetHashCode();
			num = num * -1521134295 + this.underlayOffset.x.GetHashCode();
			num = num * -1521134295 + this.underlayOffset.y.GetHashCode();
			return num * -1521134295 + this.underlaySoftness.GetHashCode();
		}

		// Token: 0x040017A9 RID: 6057
		public Color faceColor;

		// Token: 0x040017AA RID: 6058
		public Color outlineColor;

		// Token: 0x040017AB RID: 6059
		public float outlineWidth;

		// Token: 0x040017AC RID: 6060
		public Color underlayColor;

		// Token: 0x040017AD RID: 6061
		public Vector2 underlayOffset;

		// Token: 0x040017AE RID: 6062
		public float underlaySoftness;
	}
}
