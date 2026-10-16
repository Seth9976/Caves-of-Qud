using System;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public struct SoftJointLimit
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000025EC File Offset: 0x000007EC
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002604 File Offset: 0x00000804
		public float limit
		{
			get
			{
				return this.m_Limit;
			}
			set
			{
				this.m_Limit = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002610 File Offset: 0x00000810
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002628 File Offset: 0x00000828
		public float bounciness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002634 File Offset: 0x00000834
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000264C File Offset: 0x0000084C
		public float contactDistance
		{
			get
			{
				return this.m_ContactDistance;
			}
			set
			{
				this.m_ContactDistance = value;
			}
		}

		// Token: 0x04000015 RID: 21
		private float m_Limit;

		// Token: 0x04000016 RID: 22
		private float m_Bounciness;

		// Token: 0x04000017 RID: 23
		private float m_ContactDistance;
	}
}
