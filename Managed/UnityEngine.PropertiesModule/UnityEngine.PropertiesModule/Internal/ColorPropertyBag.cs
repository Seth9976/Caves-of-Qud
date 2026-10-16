using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x0200009A RID: 154
	internal class ColorPropertyBag : ContainerPropertyBag<Color>
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000BD99 File Offset: 0x00009F99
		public ColorPropertyBag()
		{
			base.AddProperty<float>(new ColorPropertyBag.RProperty());
			base.AddProperty<float>(new ColorPropertyBag.GProperty());
			base.AddProperty<float>(new ColorPropertyBag.BProperty());
			base.AddProperty<float>(new ColorPropertyBag.AProperty());
		}

		// Token: 0x0200009B RID: 155
		private class RProperty : Property<Color, float>
		{
			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600033E RID: 830 RVA: 0x0000BDD3 File Offset: 0x00009FD3
			public override string Name
			{
				get
				{
					return "r";
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600033F RID: 831 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000340 RID: 832 RVA: 0x0000BDDA File Offset: 0x00009FDA
			public override float GetValue(ref Color container)
			{
				return container.r;
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000BDE2 File Offset: 0x00009FE2
			public override void SetValue(ref Color container, float value)
			{
				container.r = value;
			}
		}

		// Token: 0x0200009C RID: 156
		private class GProperty : Property<Color, float>
		{
			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000343 RID: 835 RVA: 0x0000BDF4 File Offset: 0x00009FF4
			public override string Name
			{
				get
				{
					return "g";
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000344 RID: 836 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000345 RID: 837 RVA: 0x0000BDFB File Offset: 0x00009FFB
			public override float GetValue(ref Color container)
			{
				return container.g;
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000BE03 File Offset: 0x0000A003
			public override void SetValue(ref Color container, float value)
			{
				container.g = value;
			}
		}

		// Token: 0x0200009D RID: 157
		private class BProperty : Property<Color, float>
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BE0C File Offset: 0x0000A00C
			public override string Name
			{
				get
				{
					return "b";
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000349 RID: 841 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600034A RID: 842 RVA: 0x0000BE13 File Offset: 0x0000A013
			public override float GetValue(ref Color container)
			{
				return container.b;
			}

			// Token: 0x0600034B RID: 843 RVA: 0x0000BE1B File Offset: 0x0000A01B
			public override void SetValue(ref Color container, float value)
			{
				container.b = value;
			}
		}

		// Token: 0x0200009E RID: 158
		private class AProperty : Property<Color, float>
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600034D RID: 845 RVA: 0x0000BE24 File Offset: 0x0000A024
			public override string Name
			{
				get
				{
					return "a";
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600034E RID: 846 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000BE2B File Offset: 0x0000A02B
			public override float GetValue(ref Color container)
			{
				return container.a;
			}

			// Token: 0x06000350 RID: 848 RVA: 0x0000BE33 File Offset: 0x0000A033
			public override void SetValue(ref Color container, float value)
			{
				container.a = value;
			}
		}
	}
}
