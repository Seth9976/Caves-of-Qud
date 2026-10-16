using System;
using System.Runtime.InteropServices;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046C RID: 1132
	public struct StyleFontDefinition : IStyleValue<FontDefinition>, IEquatable<StyleFontDefinition>
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x00082F78 File Offset: 0x00081178
		// (set) Token: 0x0600236B RID: 9067 RVA: 0x00082FA3 File Offset: 0x000811A3
		public FontDefinition value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(FontDefinition);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x00082FB4 File Offset: 0x000811B4
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x00082FCC File Offset: 0x000811CC
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00082FD6 File Offset: 0x000811D6
		public StyleFontDefinition(FontDefinition f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00082FE2 File Offset: 0x000811E2
		public StyleFontDefinition(FontAsset f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00082FEE File Offset: 0x000811EE
		public StyleFontDefinition(Font f)
		{
			this = new StyleFontDefinition(f, StyleKeyword.Undefined);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00082FFC File Offset: 0x000811FC
		public StyleFontDefinition(StyleKeyword keyword)
		{
			this = new StyleFontDefinition(default(FontDefinition), keyword);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0008301B File Offset: 0x0008121B
		internal StyleFontDefinition(object obj, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromObject(obj), keyword);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0008302C File Offset: 0x0008122C
		internal StyleFontDefinition(object obj)
		{
			this = new StyleFontDefinition(FontDefinition.FromObject(obj), StyleKeyword.Undefined);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0008303D File Offset: 0x0008123D
		internal StyleFontDefinition(FontAsset f, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromSDFFont(f), keyword);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0008304E File Offset: 0x0008124E
		internal StyleFontDefinition(Font f, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(FontDefinition.FromFont(f), keyword);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00083060 File Offset: 0x00081260
		internal StyleFontDefinition(GCHandle gcHandle, StyleKeyword keyword)
		{
			this = new StyleFontDefinition(gcHandle.IsAllocated ? FontDefinition.FromObject(gcHandle.Target) : default(FontDefinition), keyword);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00083096 File Offset: 0x00081296
		internal StyleFontDefinition(FontDefinition f, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = f;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000830A7 File Offset: 0x000812A7
		internal StyleFontDefinition(StyleFontDefinition sfd)
		{
			this.m_Keyword = sfd.keyword;
			this.m_Value = sfd.value;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000830C4 File Offset: 0x000812C4
		public static implicit operator StyleFontDefinition(StyleKeyword keyword)
		{
			return new StyleFontDefinition(keyword);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000830DC File Offset: 0x000812DC
		public static implicit operator StyleFontDefinition(FontDefinition f)
		{
			return new StyleFontDefinition(f);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000830F4 File Offset: 0x000812F4
		public bool Equals(StyleFontDefinition other)
		{
			return this.m_Keyword == other.m_Keyword && this.m_Value.Equals(other.m_Value);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00083128 File Offset: 0x00081328
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleFontDefinition)
			{
				StyleFontDefinition styleFontDefinition = (StyleFontDefinition)obj;
				flag = this.Equals(styleFontDefinition);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x00083154 File Offset: 0x00081354
		public override int GetHashCode()
		{
			return (int)((this.m_Keyword * (StyleKeyword)397) ^ (StyleKeyword)this.m_Value.GetHashCode());
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x00083188 File Offset: 0x00081388
		public static bool operator ==(StyleFontDefinition left, StyleFontDefinition right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000831A4 File Offset: 0x000813A4
		public static bool operator !=(StyleFontDefinition left, StyleFontDefinition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000FC5 RID: 4037
		private StyleKeyword m_Keyword;

		// Token: 0x04000FC6 RID: 4038
		private FontDefinition m_Value;
	}
}
