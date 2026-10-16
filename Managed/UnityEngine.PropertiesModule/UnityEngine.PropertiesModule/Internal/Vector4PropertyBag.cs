using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000A6 RID: 166
	internal class Vector4PropertyBag : ContainerPropertyBag<Vector4>
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000BF08 File Offset: 0x0000A108
		public Vector4PropertyBag()
		{
			base.AddProperty<float>(new Vector4PropertyBag.XProperty());
			base.AddProperty<float>(new Vector4PropertyBag.YProperty());
			base.AddProperty<float>(new Vector4PropertyBag.ZProperty());
			base.AddProperty<float>(new Vector4PropertyBag.WProperty());
		}

		// Token: 0x020000A7 RID: 167
		private class XProperty : Property<Vector4, float>
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x0600036E RID: 878 RVA: 0x0000BE5E File Offset: 0x0000A05E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x0600036F RID: 879 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000BF42 File Offset: 0x0000A142
			public override float GetValue(ref Vector4 container)
			{
				return container.x;
			}

			// Token: 0x06000371 RID: 881 RVA: 0x0000BF4A File Offset: 0x0000A14A
			public override void SetValue(ref Vector4 container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x020000A8 RID: 168
		private class YProperty : Property<Vector4, float>
		{
			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000373 RID: 883 RVA: 0x0000BE7F File Offset: 0x0000A07F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000374 RID: 884 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000375 RID: 885 RVA: 0x0000BF5C File Offset: 0x0000A15C
			public override float GetValue(ref Vector4 container)
			{
				return container.y;
			}

			// Token: 0x06000376 RID: 886 RVA: 0x0000BF64 File Offset: 0x0000A164
			public override void SetValue(ref Vector4 container, float value)
			{
				container.y = value;
			}
		}

		// Token: 0x020000A9 RID: 169
		private class ZProperty : Property<Vector4, float>
		{
			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000378 RID: 888 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
			public override string Name
			{
				get
				{
					return "z";
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000379 RID: 889 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600037A RID: 890 RVA: 0x0000BF6D File Offset: 0x0000A16D
			public override float GetValue(ref Vector4 container)
			{
				return container.z;
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0000BF75 File Offset: 0x0000A175
			public override void SetValue(ref Vector4 container, float value)
			{
				container.z = value;
			}
		}

		// Token: 0x020000AA RID: 170
		private class WProperty : Property<Vector4, float>
		{
			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600037D RID: 893 RVA: 0x0000BF7E File Offset: 0x0000A17E
			public override string Name
			{
				get
				{
					return "w";
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600037E RID: 894 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000BF85 File Offset: 0x0000A185
			public override float GetValue(ref Vector4 container)
			{
				return container.w;
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000BF8D File Offset: 0x0000A18D
			public override void SetValue(ref Vector4 container, float value)
			{
				container.w = value;
			}
		}
	}
}
