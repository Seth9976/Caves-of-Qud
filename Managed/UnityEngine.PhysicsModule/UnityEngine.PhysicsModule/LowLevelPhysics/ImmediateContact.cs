using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x02000056 RID: 86
	public struct ImmediateContact
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public Vector3 Normal
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000BE10 File Offset: 0x0000A010
		public float Separation
		{
			get
			{
				return this.m_Separation;
			}
			set
			{
				this.m_Separation = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000BE1C File Offset: 0x0000A01C
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000BE34 File Offset: 0x0000A034
		public Vector3 Point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x04000165 RID: 357
		private Vector3 m_Normal;

		// Token: 0x04000166 RID: 358
		private float m_Separation;

		// Token: 0x04000167 RID: 359
		private Vector3 m_Point;

		// Token: 0x04000168 RID: 360
		private float m_MaxImpulse;

		// Token: 0x04000169 RID: 361
		private Vector3 m_TargetVel;

		// Token: 0x0400016A RID: 362
		private float m_StaticFriction;

		// Token: 0x0400016B RID: 363
		private byte m_MaterialFlags;

		// Token: 0x0400016C RID: 364
		private byte m_Pad;

		// Token: 0x0400016D RID: 365
		private ushort m_InternalUse;

		// Token: 0x0400016E RID: 366
		private uint m_InternalFaceIndex1;

		// Token: 0x0400016F RID: 367
		private float m_DynamicFriction;

		// Token: 0x04000170 RID: 368
		private float m_Restitution;
	}
}
