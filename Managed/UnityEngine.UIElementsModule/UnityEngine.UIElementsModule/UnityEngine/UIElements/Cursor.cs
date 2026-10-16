using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D5 RID: 469
	public struct Cursor : IEquatable<Cursor>
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0004094B File Offset: 0x0003EB4B
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x00040953 File Offset: 0x0003EB53
		public Texture2D texture { readonly get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0004095C File Offset: 0x0003EB5C
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x00040964 File Offset: 0x0003EB64
		public Vector2 hotspot { readonly get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0004096D File Offset: 0x0003EB6D
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x00040975 File Offset: 0x0003EB75
		internal int defaultCursorId { readonly get; set; }

		// Token: 0x06000E67 RID: 3687 RVA: 0x00040980 File Offset: 0x0003EB80
		public override bool Equals(object obj)
		{
			return obj is Cursor && this.Equals((Cursor)obj);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000409AC File Offset: 0x0003EBAC
		public bool Equals(Cursor other)
		{
			return EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00040A00 File Offset: 0x0003EC00
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			num = num * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot);
			return num * -1521134295 + this.defaultCursorId.GetHashCode();
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00040A64 File Offset: 0x0003EC64
		internal static IEnumerable<Type> allowedAssetTypes
		{
			get
			{
				yield return typeof(Texture2D);
				yield break;
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00040A7C File Offset: 0x0003EC7C
		public static bool operator ==(Cursor style1, Cursor style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00040A98 File Offset: 0x0003EC98
		public static bool operator !=(Cursor style1, Cursor style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00040AB4 File Offset: 0x0003ECB4
		public override string ToString()
		{
			return string.Format("texture={0}, hotspot={1}", this.texture, this.hotspot);
		}

		// Token: 0x020001D6 RID: 470
		internal class PropertyBag : ContainerPropertyBag<Cursor>
		{
			// Token: 0x06000E6E RID: 3694 RVA: 0x00040AE1 File Offset: 0x0003ECE1
			public PropertyBag()
			{
				base.AddProperty<Texture2D>(new Cursor.PropertyBag.TextureProperty());
				base.AddProperty<Vector2>(new Cursor.PropertyBag.HotspotProperty());
				base.AddProperty<int>(new Cursor.PropertyBag.DefaultCursorIdProperty());
			}

			// Token: 0x020001D7 RID: 471
			private class TextureProperty : Property<Cursor, Texture2D>
			{
				// Token: 0x17000270 RID: 624
				// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00040B0F File Offset: 0x0003ED0F
				public override string Name { get; } = "texture";

				// Token: 0x17000271 RID: 625
				// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00040B17 File Offset: 0x0003ED17
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000E71 RID: 3697 RVA: 0x00040B1F File Offset: 0x0003ED1F
				public override Texture2D GetValue(ref Cursor container)
				{
					return container.texture;
				}

				// Token: 0x06000E72 RID: 3698 RVA: 0x00040B27 File Offset: 0x0003ED27
				public override void SetValue(ref Cursor container, Texture2D value)
				{
					container.texture = value;
				}
			}

			// Token: 0x020001D8 RID: 472
			private class HotspotProperty : Property<Cursor, Vector2>
			{
				// Token: 0x17000272 RID: 626
				// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00040B4C File Offset: 0x0003ED4C
				public override string Name { get; } = "hotspot";

				// Token: 0x17000273 RID: 627
				// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00040B54 File Offset: 0x0003ED54
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000E76 RID: 3702 RVA: 0x00040B5C File Offset: 0x0003ED5C
				public override Vector2 GetValue(ref Cursor container)
				{
					return container.hotspot;
				}

				// Token: 0x06000E77 RID: 3703 RVA: 0x00040B64 File Offset: 0x0003ED64
				public override void SetValue(ref Cursor container, Vector2 value)
				{
					container.hotspot = value;
				}
			}

			// Token: 0x020001D9 RID: 473
			private class DefaultCursorIdProperty : Property<Cursor, int>
			{
				// Token: 0x17000274 RID: 628
				// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00040B89 File Offset: 0x0003ED89
				public override string Name { get; } = "defaultCursorId";

				// Token: 0x17000275 RID: 629
				// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00040B91 File Offset: 0x0003ED91
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000E7B RID: 3707 RVA: 0x00040B99 File Offset: 0x0003ED99
				public override int GetValue(ref Cursor container)
				{
					return container.defaultCursorId;
				}

				// Token: 0x06000E7C RID: 3708 RVA: 0x00040BA1 File Offset: 0x0003EDA1
				public override void SetValue(ref Cursor container, int value)
				{
					container.defaultCursorId = value;
				}
			}
		}
	}
}
