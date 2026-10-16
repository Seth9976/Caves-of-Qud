using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000A2 RID: 162
	internal class Vector3PropertyBag : ContainerPropertyBag<Vector3>
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000BE97 File Offset: 0x0000A097
		public Vector3PropertyBag()
		{
			base.AddProperty<float>(new Vector3PropertyBag.XProperty());
			base.AddProperty<float>(new Vector3PropertyBag.YProperty());
			base.AddProperty<float>(new Vector3PropertyBag.ZProperty());
		}

		// Token: 0x020000A3 RID: 163
		private class XProperty : Property<Vector3, float>
		{
			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000BE5E File Offset: 0x0000A05E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600035F RID: 863 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0000BEC5 File Offset: 0x0000A0C5
			public override float GetValue(ref Vector3 container)
			{
				return container.x;
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0000BECD File Offset: 0x0000A0CD
			public override void SetValue(ref Vector3 container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x020000A4 RID: 164
		private class YProperty : Property<Vector3, float>
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000BE7F File Offset: 0x0000A07F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000364 RID: 868 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0000BEDF File Offset: 0x0000A0DF
			public override float GetValue(ref Vector3 container)
			{
				return container.y;
			}

			// Token: 0x06000366 RID: 870 RVA: 0x0000BEE7 File Offset: 0x0000A0E7
			public override void SetValue(ref Vector3 container, float value)
			{
				container.y = value;
			}
		}

		// Token: 0x020000A5 RID: 165
		private class ZProperty : Property<Vector3, float>
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000368 RID: 872 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
			public override string Name
			{
				get
				{
					return "z";
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000369 RID: 873 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000BEF7 File Offset: 0x0000A0F7
			public override float GetValue(ref Vector3 container)
			{
				return container.z;
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0000BEFF File Offset: 0x0000A0FF
			public override void SetValue(ref Vector3 container, float value)
			{
				container.z = value;
			}
		}
	}
}
