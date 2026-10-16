using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000B2 RID: 178
	internal class RectPropertyBag : ContainerPropertyBag<Rect>
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0000C052 File Offset: 0x0000A252
		public RectPropertyBag()
		{
			base.AddProperty<float>(new RectPropertyBag.XProperty());
			base.AddProperty<float>(new RectPropertyBag.YProperty());
			base.AddProperty<float>(new RectPropertyBag.WidthProperty());
			base.AddProperty<float>(new RectPropertyBag.HeightProperty());
		}

		// Token: 0x020000B3 RID: 179
		private class XProperty : Property<Rect, float>
		{
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600039E RID: 926 RVA: 0x0000BE5E File Offset: 0x0000A05E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x0600039F RID: 927 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x0000C08C File Offset: 0x0000A28C
			public override float GetValue(ref Rect container)
			{
				return container.x;
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x0000C094 File Offset: 0x0000A294
			public override void SetValue(ref Rect container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x020000B4 RID: 180
		private class YProperty : Property<Rect, float>
		{
			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000BE7F File Offset: 0x0000A07F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x0000C0A7 File Offset: 0x0000A2A7
			public override float GetValue(ref Rect container)
			{
				return container.y;
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x0000C0AF File Offset: 0x0000A2AF
			public override void SetValue(ref Rect container, float value)
			{
				container.y = value;
			}
		}

		// Token: 0x020000B5 RID: 181
		private class WidthProperty : Property<Rect, float>
		{
			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003AA RID: 938 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
			public override float GetValue(ref Rect container)
			{
				return container.width;
			}

			// Token: 0x060003AB RID: 939 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
			public override void SetValue(ref Rect container, float value)
			{
				container.width = value;
			}
		}

		// Token: 0x020000B6 RID: 182
		private class HeightProperty : Property<Rect, float>
		{
			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060003AD RID: 941 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060003AE RID: 942 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003AF RID: 943 RVA: 0x0000C0D9 File Offset: 0x0000A2D9
			public override float GetValue(ref Rect container)
			{
				return container.height;
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x0000C0E1 File Offset: 0x0000A2E1
			public override void SetValue(ref Rect container, float value)
			{
				container.height = value;
			}
		}
	}
}
