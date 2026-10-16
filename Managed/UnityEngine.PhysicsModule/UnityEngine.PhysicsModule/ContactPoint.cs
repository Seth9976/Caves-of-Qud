using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public struct ContactPoint
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		public Collider thisCollider
		{
			get
			{
				return Physics.GetColliderByInstanceID(this.m_ThisColliderInstanceID);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020B8 File Offset: 0x000002B8
		public Collider otherCollider
		{
			get
			{
				return Physics.GetColliderByInstanceID(this.m_OtherColliderInstanceID);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F0 File Offset: 0x000002F0
		internal ContactPoint(Vector3 point, Vector3 normal, Vector3 impulse, float separation, int thisInstanceID, int otherInstenceID)
		{
			this.m_Point = point;
			this.m_Normal = normal;
			this.m_Impulse = impulse;
			this.m_Separation = separation;
			this.m_ThisColliderInstanceID = thisInstanceID;
			this.m_OtherColliderInstanceID = otherInstenceID;
		}

		// Token: 0x04000001 RID: 1
		internal Vector3 m_Point;

		// Token: 0x04000002 RID: 2
		internal Vector3 m_Normal;

		// Token: 0x04000003 RID: 3
		internal Vector3 m_Impulse;

		// Token: 0x04000004 RID: 4
		internal int m_ThisColliderInstanceID;

		// Token: 0x04000005 RID: 5
		internal int m_OtherColliderInstanceID;

		// Token: 0x04000006 RID: 6
		internal float m_Separation;
	}
}
