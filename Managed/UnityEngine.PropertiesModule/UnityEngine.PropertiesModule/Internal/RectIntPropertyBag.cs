using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000B7 RID: 183
	internal class RectIntPropertyBag : ContainerPropertyBag<RectInt>
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0000C0EB File Offset: 0x0000A2EB
		public RectIntPropertyBag()
		{
			base.AddProperty<int>(new RectIntPropertyBag.XProperty());
			base.AddProperty<int>(new RectIntPropertyBag.YProperty());
			base.AddProperty<int>(new RectIntPropertyBag.WidthProperty());
			base.AddProperty<int>(new RectIntPropertyBag.HeightProperty());
		}

		// Token: 0x020000B8 RID: 184
		private class XProperty : Property<RectInt, int>
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000BE5E File Offset: 0x0000A05E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x0000C125 File Offset: 0x0000A325
			public override int GetValue(ref RectInt container)
			{
				return container.x;
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x0000C12D File Offset: 0x0000A32D
			public override void SetValue(ref RectInt container, int value)
			{
				container.x = value;
			}
		}

		// Token: 0x020000B9 RID: 185
		private class YProperty : Property<RectInt, int>
		{
			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000BE7F File Offset: 0x0000A07F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003BA RID: 954 RVA: 0x0000C140 File Offset: 0x0000A340
			public override int GetValue(ref RectInt container)
			{
				return container.y;
			}

			// Token: 0x060003BB RID: 955 RVA: 0x0000C148 File Offset: 0x0000A348
			public override void SetValue(ref RectInt container, int value)
			{
				container.y = value;
			}
		}

		// Token: 0x020000BA RID: 186
		private class WidthProperty : Property<RectInt, int>
		{
			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060003BD RID: 957 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
			public override string Name
			{
				get
				{
					return "width";
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060003BE RID: 958 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003BF RID: 959 RVA: 0x0000C152 File Offset: 0x0000A352
			public override int GetValue(ref RectInt container)
			{
				return container.width;
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x0000C15A File Offset: 0x0000A35A
			public override void SetValue(ref RectInt container, int value)
			{
				container.width = value;
			}
		}

		// Token: 0x020000BB RID: 187
		private class HeightProperty : Property<RectInt, int>
		{
			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
			public override string Name
			{
				get
				{
					return "height";
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060003C3 RID: 963 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x0000C164 File Offset: 0x0000A364
			public override int GetValue(ref RectInt container)
			{
				return container.height;
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x0000C16C File Offset: 0x0000A36C
			public override void SetValue(ref RectInt container, int value)
			{
				container.height = value;
			}
		}
	}
}
