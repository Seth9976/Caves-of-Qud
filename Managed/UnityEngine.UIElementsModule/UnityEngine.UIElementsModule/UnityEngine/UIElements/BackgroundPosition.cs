using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000A RID: 10
	public struct BackgroundPosition : IEquatable<BackgroundPosition>
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002B4B File Offset: 0x00000D4B
		public BackgroundPosition(BackgroundPositionKeyword keyword)
		{
			this.keyword = keyword;
			this.offset = new Length(0f);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B65 File Offset: 0x00000D65
		public BackgroundPosition(BackgroundPositionKeyword keyword, Length offset)
		{
			this.keyword = keyword;
			this.offset = offset;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B78 File Offset: 0x00000D78
		internal static BackgroundPosition Initial()
		{
			return BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(ScaleMode.StretchToFill);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B90 File Offset: 0x00000D90
		public override bool Equals(object obj)
		{
			return obj is BackgroundPosition && this.Equals((BackgroundPosition)obj);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BBC File Offset: 0x00000DBC
		public bool Equals(BackgroundPosition other)
		{
			return other.offset == this.offset && other.keyword == this.keyword;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + this.keyword.GetHashCode();
			return num * -1521134295 + this.offset.GetHashCode();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C44 File Offset: 0x00000E44
		public static bool operator ==(BackgroundPosition style1, BackgroundPosition style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C60 File Offset: 0x00000E60
		public static bool operator !=(BackgroundPosition style1, BackgroundPosition style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C7C File Offset: 0x00000E7C
		public override string ToString()
		{
			return string.Format("(type:{0} x:{1})", this.keyword, this.offset);
		}

		// Token: 0x04000018 RID: 24
		public BackgroundPositionKeyword keyword;

		// Token: 0x04000019 RID: 25
		public Length offset;

		// Token: 0x0200000B RID: 11
		internal class PropertyBag : ContainerPropertyBag<BackgroundPosition>
		{
			// Token: 0x06000043 RID: 67 RVA: 0x00002CAE File Offset: 0x00000EAE
			public PropertyBag()
			{
				base.AddProperty<BackgroundPositionKeyword>(new BackgroundPosition.PropertyBag.KeywordProperty());
				base.AddProperty<Length>(new BackgroundPosition.PropertyBag.OffsetProperty());
			}

			// Token: 0x0200000C RID: 12
			private class KeywordProperty : Property<BackgroundPosition, BackgroundPositionKeyword>
			{
				// Token: 0x1700000B RID: 11
				// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CD0 File Offset: 0x00000ED0
				public override string Name { get; } = "keyword";

				// Token: 0x1700000C RID: 12
				// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CD8 File Offset: 0x00000ED8
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000046 RID: 70 RVA: 0x00002CE0 File Offset: 0x00000EE0
				public override BackgroundPositionKeyword GetValue(ref BackgroundPosition container)
				{
					return container.keyword;
				}

				// Token: 0x06000047 RID: 71 RVA: 0x00002CE8 File Offset: 0x00000EE8
				public override void SetValue(ref BackgroundPosition container, BackgroundPositionKeyword value)
				{
					container.keyword = value;
				}
			}

			// Token: 0x0200000D RID: 13
			private class OffsetProperty : Property<BackgroundPosition, Length>
			{
				// Token: 0x1700000D RID: 13
				// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D0C File Offset: 0x00000F0C
				public override string Name { get; } = "offset";

				// Token: 0x1700000E RID: 14
				// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D14 File Offset: 0x00000F14
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600004B RID: 75 RVA: 0x00002D1C File Offset: 0x00000F1C
				public override Length GetValue(ref BackgroundPosition container)
				{
					return container.offset;
				}

				// Token: 0x0600004C RID: 76 RVA: 0x00002D24 File Offset: 0x00000F24
				public override void SetValue(ref BackgroundPosition container, Length value)
				{
					container.offset = value;
				}
			}
		}
	}
}
