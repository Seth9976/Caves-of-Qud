using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000010 RID: 16
	[MovedFrom("UnityEngine")]
	public struct NavMeshHit
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003B5C File Offset: 0x00001D5C
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00003B64 File Offset: 0x00001D64
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003B6D File Offset: 0x00001D6D
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00003B75 File Offset: 0x00001D75
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003B7E File Offset: 0x00001D7E
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00003B86 File Offset: 0x00001D86
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003B8F File Offset: 0x00001D8F
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00003B97 File Offset: 0x00001D97
		public int mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003BA0 File Offset: 0x00001DA0
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00003BAB File Offset: 0x00001DAB
		public bool hit
		{
			get
			{
				return this.m_Hit != 0;
			}
			set
			{
				this.m_Hit = (value ? 1 : 0);
			}
		}

		// Token: 0x04000028 RID: 40
		private Vector3 m_Position;

		// Token: 0x04000029 RID: 41
		private Vector3 m_Normal;

		// Token: 0x0400002A RID: 42
		private float m_Distance;

		// Token: 0x0400002B RID: 43
		private int m_Mask;

		// Token: 0x0400002C RID: 44
		private int m_Hit;
	}
}
