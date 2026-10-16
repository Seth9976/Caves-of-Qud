using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044C RID: 1100
	internal struct InheritedData : IStyleDataGroup<InheritedData>, IEquatable<InheritedData>
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x0007B43C File Offset: 0x0007963C
		public InheritedData Copy()
		{
			return this;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0007B454 File Offset: 0x00079654
		public void CopyFrom(ref InheritedData other)
		{
			this = other;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0007B464 File Offset: 0x00079664
		public static bool operator ==(InheritedData lhs, InheritedData rhs)
		{
			return lhs.color == rhs.color && lhs.fontSize == rhs.fontSize && lhs.letterSpacing == rhs.letterSpacing && lhs.textShadow == rhs.textShadow && lhs.unityEditorTextRenderingMode == rhs.unityEditorTextRenderingMode && lhs.unityFont == rhs.unityFont && lhs.unityFontDefinition == rhs.unityFontDefinition && lhs.unityFontStyleAndWeight == rhs.unityFontStyleAndWeight && lhs.unityParagraphSpacing == rhs.unityParagraphSpacing && lhs.unityTextAlign == rhs.unityTextAlign && lhs.unityTextGenerator == rhs.unityTextGenerator && lhs.unityTextOutlineColor == rhs.unityTextOutlineColor && lhs.unityTextOutlineWidth == rhs.unityTextOutlineWidth && lhs.visibility == rhs.visibility && lhs.whiteSpace == rhs.whiteSpace && lhs.wordSpacing == rhs.wordSpacing;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0007B59C File Offset: 0x0007979C
		public static bool operator !=(InheritedData lhs, InheritedData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0007B5B8 File Offset: 0x000797B8
		public bool Equals(InheritedData other)
		{
			return other == this;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0007B5D8 File Offset: 0x000797D8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is InheritedData && this.Equals((InheritedData)obj);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0007B610 File Offset: 0x00079810
		public override int GetHashCode()
		{
			int num = this.color.GetHashCode();
			num = (num * 397) ^ this.fontSize.GetHashCode();
			num = (num * 397) ^ this.letterSpacing.GetHashCode();
			num = (num * 397) ^ this.textShadow.GetHashCode();
			num = (num * 397) ^ (int)this.unityEditorTextRenderingMode;
			num = (num * 397) ^ ((this.unityFont == null) ? 0 : this.unityFont.GetHashCode());
			num = (num * 397) ^ this.unityFontDefinition.GetHashCode();
			num = (num * 397) ^ (int)this.unityFontStyleAndWeight;
			num = (num * 397) ^ this.unityParagraphSpacing.GetHashCode();
			num = (num * 397) ^ (int)this.unityTextAlign;
			num = (num * 397) ^ (int)this.unityTextGenerator;
			num = (num * 397) ^ this.unityTextOutlineColor.GetHashCode();
			num = (num * 397) ^ this.unityTextOutlineWidth.GetHashCode();
			num = (num * 397) ^ (int)this.visibility;
			num = (num * 397) ^ (int)this.whiteSpace;
			return (num * 397) ^ this.wordSpacing.GetHashCode();
		}

		// Token: 0x04000F3B RID: 3899
		public Color color;

		// Token: 0x04000F3C RID: 3900
		public Length fontSize;

		// Token: 0x04000F3D RID: 3901
		public Length letterSpacing;

		// Token: 0x04000F3E RID: 3902
		public TextShadow textShadow;

		// Token: 0x04000F3F RID: 3903
		public EditorTextRenderingMode unityEditorTextRenderingMode;

		// Token: 0x04000F40 RID: 3904
		public Font unityFont;

		// Token: 0x04000F41 RID: 3905
		public FontDefinition unityFontDefinition;

		// Token: 0x04000F42 RID: 3906
		public FontStyle unityFontStyleAndWeight;

		// Token: 0x04000F43 RID: 3907
		public Length unityParagraphSpacing;

		// Token: 0x04000F44 RID: 3908
		public TextAnchor unityTextAlign;

		// Token: 0x04000F45 RID: 3909
		public TextGeneratorType unityTextGenerator;

		// Token: 0x04000F46 RID: 3910
		public Color unityTextOutlineColor;

		// Token: 0x04000F47 RID: 3911
		public float unityTextOutlineWidth;

		// Token: 0x04000F48 RID: 3912
		public Visibility visibility;

		// Token: 0x04000F49 RID: 3913
		public WhiteSpace whiteSpace;

		// Token: 0x04000F4A RID: 3914
		public Length wordSpacing;
	}
}
