using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000BC RID: 188
	internal class BoundsPropertyBag : ContainerPropertyBag<Bounds>
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000C176 File Offset: 0x0000A376
		public BoundsPropertyBag()
		{
			base.AddProperty<Vector3>(new BoundsPropertyBag.CenterProperty());
			base.AddProperty<Vector3>(new BoundsPropertyBag.ExtentsProperty());
		}

		// Token: 0x020000BD RID: 189
		private class CenterProperty : Property<Bounds, Vector3>
		{
			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000C198 File Offset: 0x0000A398
			public override string Name
			{
				get
				{
					return "center";
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060003C9 RID: 969 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x0000C19F File Offset: 0x0000A39F
			public override Vector3 GetValue(ref Bounds container)
			{
				return container.center;
			}

			// Token: 0x060003CB RID: 971 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
			public override void SetValue(ref Bounds container, Vector3 value)
			{
				container.center = value;
			}
		}

		// Token: 0x020000BE RID: 190
		private class ExtentsProperty : Property<Bounds, Vector3>
		{
			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060003CD RID: 973 RVA: 0x0000C1BA File Offset: 0x0000A3BA
			public override string Name
			{
				get
				{
					return "extents";
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060003CE RID: 974 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003CF RID: 975 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
			public override Vector3 GetValue(ref Bounds container)
			{
				return container.extents;
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
			public override void SetValue(ref Bounds container, Vector3 value)
			{
				container.extents = value;
			}
		}
	}
}
