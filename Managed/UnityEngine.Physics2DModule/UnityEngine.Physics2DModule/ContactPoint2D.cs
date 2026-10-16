using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[NativeClass("ScriptingContactPoint2D", "struct ScriptingContactPoint2D;")]
	[RequiredByNativeCode(Optional = false, GenerateProxy = true)]
	[NativeHeader("Modules/Physics2D/Public/PhysicsScripting2D.h")]
	public struct ContactPoint2D
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00007C58 File Offset: 0x00005E58
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00007C70 File Offset: 0x00005E70
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00007C88 File Offset: 0x00005E88
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public float normalImpulse
		{
			get
			{
				return this.m_NormalImpulse;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public float tangentImpulse
		{
			get
			{
				return this.m_TangentImpulse;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public float friction
		{
			get
			{
				return this.m_Friction;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00007D00 File Offset: 0x00005F00
		public float bounciness
		{
			get
			{
				return this.m_Bounciness;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00007D18 File Offset: 0x00005F18
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00007D3C File Offset: 0x00005F3C
		public Collider2D otherCollider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherCollider) as Collider2D;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00007D60 File Offset: 0x00005F60
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00007D84 File Offset: 0x00005F84
		public Rigidbody2D otherRigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherRigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public bool enabled
		{
			get
			{
				return this.m_Enabled == 1;
			}
		}

		// Token: 0x0400007A RID: 122
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x0400007B RID: 123
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x0400007C RID: 124
		[NativeName("relativeVelocity")]
		private Vector2 m_RelativeVelocity;

		// Token: 0x0400007D RID: 125
		[NativeName("friction")]
		private float m_Friction;

		// Token: 0x0400007E RID: 126
		[NativeName("bounciness")]
		private float m_Bounciness;

		// Token: 0x0400007F RID: 127
		[NativeName("separation")]
		private float m_Separation;

		// Token: 0x04000080 RID: 128
		[NativeName("normalImpulse")]
		private float m_NormalImpulse;

		// Token: 0x04000081 RID: 129
		[NativeName("tangentImpulse")]
		private float m_TangentImpulse;

		// Token: 0x04000082 RID: 130
		[NativeName("collider")]
		private int m_Collider;

		// Token: 0x04000083 RID: 131
		[NativeName("otherCollider")]
		private int m_OtherCollider;

		// Token: 0x04000084 RID: 132
		[NativeName("rigidbody")]
		private int m_Rigidbody;

		// Token: 0x04000085 RID: 133
		[NativeName("otherRigidbody")]
		private int m_OtherRigidbody;

		// Token: 0x04000086 RID: 134
		[NativeName("enabled")]
		private int m_Enabled;
	}
}
