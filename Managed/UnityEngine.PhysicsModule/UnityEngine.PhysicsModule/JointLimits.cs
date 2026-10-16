using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public struct JointLimits
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000027DC File Offset: 0x000009DC
		public float min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027E8 File Offset: 0x000009E8
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002800 File Offset: 0x00000A00
		public float max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000280C File Offset: 0x00000A0C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002824 File Offset: 0x00000A24
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002830 File Offset: 0x00000A30
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002848 File Offset: 0x00000A48
		public float bounceMinVelocity
		{
			get
			{
				return this.m_BounceMinVelocity;
			}
			set
			{
				this.m_BounceMinVelocity = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000286C File Offset: 0x00000A6C
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

		// Token: 0x04000024 RID: 36
		private float m_Min;

		// Token: 0x04000025 RID: 37
		private float m_Max;

		// Token: 0x04000026 RID: 38
		private float m_Bounciness;

		// Token: 0x04000027 RID: 39
		private float m_BounceMinVelocity;

		// Token: 0x04000028 RID: 40
		private float m_ContactDistance;

		// Token: 0x04000029 RID: 41
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
		public float minBounce;

		// Token: 0x0400002A RID: 42
		[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float maxBounce;
	}
}
