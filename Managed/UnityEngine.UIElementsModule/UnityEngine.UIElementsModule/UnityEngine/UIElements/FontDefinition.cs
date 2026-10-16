using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000357 RID: 855
	public struct FontDefinition : IEquatable<FontDefinition>
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00069C68 File Offset: 0x00067E68
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x00069C80 File Offset: 0x00067E80
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				bool flag = value != null && this.fontAsset != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_Font = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00069CBC File Offset: 0x00067EBC
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x00069CD4 File Offset: 0x00067ED4
		public FontAsset fontAsset
		{
			get
			{
				return this.m_FontAsset;
			}
			set
			{
				bool flag = value != null && this.font != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_FontAsset = value;
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00069D10 File Offset: 0x00067F10
		public static FontDefinition FromFont(Font f)
		{
			return new FontDefinition
			{
				m_Font = f
			};
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00069D34 File Offset: 0x00067F34
		public static FontDefinition FromSDFFont(FontAsset f)
		{
			return new FontDefinition
			{
				m_FontAsset = f
			};
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00069D58 File Offset: 0x00067F58
		internal static FontDefinition FromObject(object obj)
		{
			Font font = obj as Font;
			bool flag = font != null;
			FontDefinition fontDefinition;
			if (flag)
			{
				fontDefinition = FontDefinition.FromFont(font);
			}
			else
			{
				FontAsset fontAsset = obj as FontAsset;
				bool flag2 = fontAsset != null;
				if (flag2)
				{
					fontDefinition = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					fontDefinition = default(FontDefinition);
				}
			}
			return fontDefinition;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00069DAC File Offset: 0x00067FAC
		internal static IEnumerable<Type> allowedAssetTypes
		{
			get
			{
				yield return typeof(Font);
				yield return typeof(FontAsset);
				yield break;
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00069DC4 File Offset: 0x00067FC4
		internal bool IsEmpty()
		{
			return this.m_Font == null && this.m_FontAsset == null;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00069DF4 File Offset: 0x00067FF4
		public override string ToString()
		{
			bool flag = this.font != null;
			string text;
			if (flag)
			{
				text = string.Format("{0}", this.font);
			}
			else
			{
				text = string.Format("{0}", this.fontAsset);
			}
			return text;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00069E3C File Offset: 0x0006803C
		public bool Equals(FontDefinition other)
		{
			return object.Equals(this.m_Font, other.m_Font) && object.Equals(this.m_FontAsset, other.m_FontAsset);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00069E78 File Offset: 0x00068078
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is FontDefinition)
			{
				FontDefinition fontDefinition = (FontDefinition)obj;
				flag = this.Equals(fontDefinition);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00069EA4 File Offset: 0x000680A4
		public override int GetHashCode()
		{
			return (((this.m_Font != null) ? this.m_Font.GetHashCode() : 0) * 397) ^ ((this.m_FontAsset != null) ? this.m_FontAsset.GetHashCode() : 0);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00069EF8 File Offset: 0x000680F8
		public static bool operator ==(FontDefinition left, FontDefinition right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00069F14 File Offset: 0x00068114
		public static bool operator !=(FontDefinition left, FontDefinition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D8C RID: 3468
		private Font m_Font;

		// Token: 0x04000D8D RID: 3469
		private FontAsset m_FontAsset;

		// Token: 0x02000358 RID: 856
		internal class PropertyBag : ContainerPropertyBag<FontDefinition>
		{
			// Token: 0x06001958 RID: 6488 RVA: 0x00069F31 File Offset: 0x00068131
			public PropertyBag()
			{
				base.AddProperty<Font>(new FontDefinition.PropertyBag.FontProperty());
				base.AddProperty<FontAsset>(new FontDefinition.PropertyBag.FontAssetProperty());
			}

			// Token: 0x02000359 RID: 857
			private class FontProperty : Property<FontDefinition, Font>
			{
				// Token: 0x17000504 RID: 1284
				// (get) Token: 0x06001959 RID: 6489 RVA: 0x00069F53 File Offset: 0x00068153
				public override string Name { get; } = "font";

				// Token: 0x17000505 RID: 1285
				// (get) Token: 0x0600195A RID: 6490 RVA: 0x00069F5B File Offset: 0x0006815B
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600195B RID: 6491 RVA: 0x00069F63 File Offset: 0x00068163
				public override Font GetValue(ref FontDefinition container)
				{
					return container.font;
				}

				// Token: 0x0600195C RID: 6492 RVA: 0x00069F6B File Offset: 0x0006816B
				public override void SetValue(ref FontDefinition container, Font value)
				{
					container.font = value;
				}
			}

			// Token: 0x0200035A RID: 858
			private class FontAssetProperty : Property<FontDefinition, FontAsset>
			{
				// Token: 0x17000506 RID: 1286
				// (get) Token: 0x0600195E RID: 6494 RVA: 0x00069F90 File Offset: 0x00068190
				public override string Name { get; } = "fontAsset";

				// Token: 0x17000507 RID: 1287
				// (get) Token: 0x0600195F RID: 6495 RVA: 0x00069F98 File Offset: 0x00068198
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001960 RID: 6496 RVA: 0x00069FA0 File Offset: 0x000681A0
				public override FontAsset GetValue(ref FontDefinition container)
				{
					return container.fontAsset;
				}

				// Token: 0x06001961 RID: 6497 RVA: 0x00069FA8 File Offset: 0x000681A8
				public override void SetValue(ref FontDefinition container, FontAsset value)
				{
					container.fontAsset = value;
				}
			}
		}
	}
}
