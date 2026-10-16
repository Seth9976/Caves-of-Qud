using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ControllerColliderHit
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000047DC File Offset: 0x000029DC
		public CharacterController controller
		{
			get
			{
				return this.m_Controller;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000047F4 File Offset: 0x000029F4
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000480C File Offset: 0x00002A0C
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Collider.attachedRigidbody;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000482C File Offset: 0x00002A2C
		public GameObject gameObject
		{
			get
			{
				return this.m_Collider.gameObject;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000484C File Offset: 0x00002A4C
		public Transform transform
		{
			get
			{
				return this.m_Collider.transform;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000486C File Offset: 0x00002A6C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00004884 File Offset: 0x00002A84
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000489C File Offset: 0x00002A9C
		public Vector3 moveDirection
		{
			get
			{
				return this.m_MoveDirection;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000048B4 File Offset: 0x00002AB4
		public float moveLength
		{
			get
			{
				return this.m_MoveLength;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000048CC File Offset: 0x00002ACC
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000048E7 File Offset: 0x00002AE7
		private bool push
		{
			get
			{
				return this.m_Push != 0;
			}
			set
			{
				this.m_Push = (value ? 1 : 0);
			}
		}

		// Token: 0x0400005D RID: 93
		internal CharacterController m_Controller;

		// Token: 0x0400005E RID: 94
		internal Collider m_Collider;

		// Token: 0x0400005F RID: 95
		internal Vector3 m_Point;

		// Token: 0x04000060 RID: 96
		internal Vector3 m_Normal;

		// Token: 0x04000061 RID: 97
		internal Vector3 m_MoveDirection;

		// Token: 0x04000062 RID: 98
		internal float m_MoveLength;

		// Token: 0x04000063 RID: 99
		internal int m_Push;
	}
}
