using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000F RID: 15
	public struct BackgroundRepeat : IEquatable<BackgroundRepeat>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002EB9 File Offset: 0x000010B9
		public BackgroundRepeat(Repeat repeatX, Repeat repeatY)
		{
			this.x = repeatX;
			this.y = repeatY;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002ECC File Offset: 0x000010CC
		internal static BackgroundRepeat Initial()
		{
			return BackgroundPropertyHelper.ConvertScaleModeToBackgroundRepeat(ScaleMode.StretchToFill);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EE4 File Offset: 0x000010E4
		public override bool Equals(object obj)
		{
			return obj is BackgroundRepeat && this.Equals((BackgroundRepeat)obj);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F10 File Offset: 0x00001110
		public bool Equals(BackgroundRepeat other)
		{
			return other.x == this.x && other.y == this.y;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F44 File Offset: 0x00001144
		public override int GetHashCode()
		{
			int num = 1500536833;
			num = num * -1521134295 + this.x.GetHashCode();
			return num * -1521134295 + this.y.GetHashCode();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F94 File Offset: 0x00001194
		public static bool operator ==(BackgroundRepeat style1, BackgroundRepeat style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FB0 File Offset: 0x000011B0
		public static bool operator !=(BackgroundRepeat style1, BackgroundRepeat style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FCC File Offset: 0x000011CC
		public override string ToString()
		{
			return string.Format("(x:{0}, y:{1})", this.x, this.y);
		}

		// Token: 0x0400001E RID: 30
		public Repeat x;

		// Token: 0x0400001F RID: 31
		public Repeat y;

		// Token: 0x02000010 RID: 16
		internal class PropertyBag : ContainerPropertyBag<BackgroundRepeat>
		{
			// Token: 0x0600005A RID: 90 RVA: 0x00002FFE File Offset: 0x000011FE
			public PropertyBag()
			{
				base.AddProperty<Repeat>(new BackgroundRepeat.PropertyBag.XProperty());
				base.AddProperty<Repeat>(new BackgroundRepeat.PropertyBag.YProperty());
			}

			// Token: 0x02000011 RID: 17
			private class XProperty : Property<BackgroundRepeat, Repeat>
			{
				// Token: 0x1700000F RID: 15
				// (get) Token: 0x0600005B RID: 91 RVA: 0x00003020 File Offset: 0x00001220
				public override string Name { get; } = "x";

				// Token: 0x17000010 RID: 16
				// (get) Token: 0x0600005C RID: 92 RVA: 0x00003028 File Offset: 0x00001228
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600005D RID: 93 RVA: 0x00003030 File Offset: 0x00001230
				public override Repeat GetValue(ref BackgroundRepeat container)
				{
					return container.x;
				}

				// Token: 0x0600005E RID: 94 RVA: 0x00003038 File Offset: 0x00001238
				public override void SetValue(ref BackgroundRepeat container, Repeat value)
				{
					container.x = value;
				}
			}

			// Token: 0x02000012 RID: 18
			private class YProperty : Property<BackgroundRepeat, Repeat>
			{
				// Token: 0x17000011 RID: 17
				// (get) Token: 0x06000060 RID: 96 RVA: 0x0000305C File Offset: 0x0000125C
				public override string Name { get; } = "y";

				// Token: 0x17000012 RID: 18
				// (get) Token: 0x06000061 RID: 97 RVA: 0x00003064 File Offset: 0x00001264
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000062 RID: 98 RVA: 0x0000306C File Offset: 0x0000126C
				public override Repeat GetValue(ref BackgroundRepeat container)
				{
					return container.y;
				}

				// Token: 0x06000063 RID: 99 RVA: 0x00003074 File Offset: 0x00001274
				public override void SetValue(ref BackgroundRepeat container, Repeat value)
				{
					container.y = value;
				}
			}
		}
	}
}
