using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x0200009F RID: 159
	internal class Vector2PropertyBag : ContainerPropertyBag<Vector2>
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000BE3C File Offset: 0x0000A03C
		public Vector2PropertyBag()
		{
			base.AddProperty<float>(new Vector2PropertyBag.XProperty());
			base.AddProperty<float>(new Vector2PropertyBag.YProperty());
		}

		// Token: 0x020000A0 RID: 160
		private class XProperty : Property<Vector2, float>
		{
			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000353 RID: 851 RVA: 0x0000BE5E File Offset: 0x0000A05E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000354 RID: 852 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0000BE65 File Offset: 0x0000A065
			public override float GetValue(ref Vector2 container)
			{
				return container.x;
			}

			// Token: 0x06000356 RID: 854 RVA: 0x0000BE6D File Offset: 0x0000A06D
			public override void SetValue(ref Vector2 container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x020000A1 RID: 161
		private class YProperty : Property<Vector2, float>
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BE7F File Offset: 0x0000A07F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000359 RID: 857 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000BE86 File Offset: 0x0000A086
			public override float GetValue(ref Vector2 container)
			{
				return container.y;
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000BE8E File Offset: 0x0000A08E
			public override void SetValue(ref Vector2 container, float value)
			{
				container.y = value;
			}
		}
	}
}
