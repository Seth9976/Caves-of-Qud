using System;

namespace UnityEngine
{
	// Token: 0x02000046 RID: 70
	public struct ColliderHit
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00009A5B File Offset: 0x00007C5B
		public int instanceID
		{
			get
			{
				return this.m_ColliderInstanceID;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00009A63 File Offset: 0x00007C63
		public Collider collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.instanceID) as Collider;
			}
		}

		// Token: 0x04000121 RID: 289
		private int m_ColliderInstanceID;
	}
}
