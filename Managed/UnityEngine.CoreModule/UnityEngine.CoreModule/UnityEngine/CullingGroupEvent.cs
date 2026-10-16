using System;

namespace UnityEngine
{
	// Token: 0x0200012B RID: 299
	public struct CullingGroupEvent
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0000E22C File Offset: 0x0000C42C
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0000E244 File Offset: 0x0000C444
		public bool isVisible
		{
			get
			{
				return (this.m_ThisState & 128) > 0;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0000E268 File Offset: 0x0000C468
		public bool wasVisible
		{
			get
			{
				return (this.m_PrevState & 128) > 0;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0000E28C File Offset: 0x0000C48C
		public bool hasBecomeVisible
		{
			get
			{
				return this.isVisible && !this.wasVisible;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public bool hasBecomeInvisible
		{
			get
			{
				return !this.isVisible && this.wasVisible;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		public int currentDistance
		{
			get
			{
				return (int)(this.m_ThisState & 127);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public int previousDistance
		{
			get
			{
				return (int)(this.m_PrevState & 127);
			}
		}

		// Token: 0x04000404 RID: 1028
		private int m_Index;

		// Token: 0x04000405 RID: 1029
		private byte m_PrevState;

		// Token: 0x04000406 RID: 1030
		private byte m_ThisState;

		// Token: 0x04000407 RID: 1031
		private const byte kIsVisibleMask = 128;

		// Token: 0x04000408 RID: 1032
		private const byte kDistanceMask = 127;
	}
}
