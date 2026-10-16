using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000500 RID: 1280
	public struct TextShadow : IEquatable<TextShadow>
	{
		// Token: 0x060027CD RID: 10189 RVA: 0x00091978 File Offset: 0x0008FB78
		public override bool Equals(object obj)
		{
			return obj is TextShadow && this.Equals((TextShadow)obj);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000919A4 File Offset: 0x0008FBA4
		public bool Equals(TextShadow other)
		{
			return other.offset == this.offset && other.blurRadius == this.blurRadius && other.color == this.color;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000919EC File Offset: 0x0008FBEC
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + this.offset.GetHashCode();
			num = num * -1521134295 + this.blurRadius.GetHashCode();
			return num * -1521134295 + this.color.GetHashCode();
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x00091A50 File Offset: 0x0008FC50
		public static bool operator ==(TextShadow style1, TextShadow style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x00091A6C File Offset: 0x0008FC6C
		public static bool operator !=(TextShadow style1, TextShadow style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x00091A88 File Offset: 0x0008FC88
		public override string ToString()
		{
			return string.Format("offset={0}, blurRadius={1}, color={2}", this.offset, this.blurRadius, this.color);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x00091AC8 File Offset: 0x0008FCC8
		internal static TextShadow LerpUnclamped(TextShadow a, TextShadow b, float t)
		{
			return new TextShadow
			{
				offset = Vector2.LerpUnclamped(a.offset, b.offset, t),
				blurRadius = Mathf.LerpUnclamped(a.blurRadius, b.blurRadius, t),
				color = Color.LerpUnclamped(a.color, b.color, t)
			};
		}

		// Token: 0x04001208 RID: 4616
		public Vector2 offset;

		// Token: 0x04001209 RID: 4617
		public float blurRadius;

		// Token: 0x0400120A RID: 4618
		public Color color;

		// Token: 0x02000501 RID: 1281
		internal class PropertyBag : ContainerPropertyBag<TextShadow>
		{
			// Token: 0x060027D4 RID: 10196 RVA: 0x00091B2E File Offset: 0x0008FD2E
			public PropertyBag()
			{
				base.AddProperty<Vector2>(new TextShadow.PropertyBag.OffsetProperty());
				base.AddProperty<float>(new TextShadow.PropertyBag.BlurRadiusProperty());
				base.AddProperty<Color>(new TextShadow.PropertyBag.ColorProperty());
			}

			// Token: 0x02000502 RID: 1282
			private class OffsetProperty : Property<TextShadow, Vector2>
			{
				// Token: 0x17000A20 RID: 2592
				// (get) Token: 0x060027D5 RID: 10197 RVA: 0x00091B5C File Offset: 0x0008FD5C
				public override string Name { get; } = "offset";

				// Token: 0x17000A21 RID: 2593
				// (get) Token: 0x060027D6 RID: 10198 RVA: 0x00091B64 File Offset: 0x0008FD64
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060027D7 RID: 10199 RVA: 0x00091B6C File Offset: 0x0008FD6C
				public override Vector2 GetValue(ref TextShadow container)
				{
					return container.offset;
				}

				// Token: 0x060027D8 RID: 10200 RVA: 0x00091B74 File Offset: 0x0008FD74
				public override void SetValue(ref TextShadow container, Vector2 value)
				{
					container.offset = value;
				}
			}

			// Token: 0x02000503 RID: 1283
			private class BlurRadiusProperty : Property<TextShadow, float>
			{
				// Token: 0x17000A22 RID: 2594
				// (get) Token: 0x060027DA RID: 10202 RVA: 0x00091B98 File Offset: 0x0008FD98
				public override string Name { get; } = "blurRadius";

				// Token: 0x17000A23 RID: 2595
				// (get) Token: 0x060027DB RID: 10203 RVA: 0x00091BA0 File Offset: 0x0008FDA0
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060027DC RID: 10204 RVA: 0x00091BA8 File Offset: 0x0008FDA8
				public override float GetValue(ref TextShadow container)
				{
					return container.blurRadius;
				}

				// Token: 0x060027DD RID: 10205 RVA: 0x00091BB0 File Offset: 0x0008FDB0
				public override void SetValue(ref TextShadow container, float value)
				{
					container.blurRadius = value;
				}
			}

			// Token: 0x02000504 RID: 1284
			private class ColorProperty : Property<TextShadow, Color>
			{
				// Token: 0x17000A24 RID: 2596
				// (get) Token: 0x060027DF RID: 10207 RVA: 0x00091BD4 File Offset: 0x0008FDD4
				public override string Name { get; } = "color";

				// Token: 0x17000A25 RID: 2597
				// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00091BDC File Offset: 0x0008FDDC
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060027E1 RID: 10209 RVA: 0x00091BE4 File Offset: 0x0008FDE4
				public override Color GetValue(ref TextShadow container)
				{
					return container.color;
				}

				// Token: 0x060027E2 RID: 10210 RVA: 0x00091BEC File Offset: 0x0008FDEC
				public override void SetValue(ref TextShadow container, Color value)
				{
					container.color = value;
				}
			}
		}
	}
}
