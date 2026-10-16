using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x020000AB RID: 171
	internal class Vector2IntPropertyBag : ContainerPropertyBag<Vector2Int>
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000BF96 File Offset: 0x0000A196
		public Vector2IntPropertyBag()
		{
			base.AddProperty<int>(new Vector2IntPropertyBag.XProperty());
			base.AddProperty<int>(new Vector2IntPropertyBag.YProperty());
		}

		// Token: 0x020000AC RID: 172
		private class XProperty : Property<Vector2Int, int>
		{
			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000383 RID: 899 RVA: 0x0000BE5E File Offset: 0x0000A05E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000384 RID: 900 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
			public override int GetValue(ref Vector2Int container)
			{
				return container.x;
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
			public override void SetValue(ref Vector2Int container, int value)
			{
				container.x = value;
			}
		}

		// Token: 0x020000AD RID: 173
		private class YProperty : Property<Vector2Int, int>
		{
			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000388 RID: 904 RVA: 0x0000BE7F File Offset: 0x0000A07F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000389 RID: 905 RVA: 0x000057A5 File Offset: 0x000039A5
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600038A RID: 906 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
			public override int GetValue(ref Vector2Int container)
			{
				return container.y;
			}

			// Token: 0x0600038B RID: 907 RVA: 0x0000BFDB File Offset: 0x0000A1DB
			public override void SetValue(ref Vector2Int container, int value)
			{
				container.y = value;
			}
		}
	}
}
