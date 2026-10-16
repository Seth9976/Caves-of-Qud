using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("RaycastHit2D", "struct RaycastHit2D;")]
	[NativeHeader("Runtime/Interfaces/IPhysics2D.h")]
	public struct RaycastHit2D
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007F08 File Offset: 0x00006108
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00007F20 File Offset: 0x00006120
		public Vector2 centroid
		{
			get
			{
				return this.m_Centroid;
			}
			set
			{
				this.m_Centroid = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00007F2C File Offset: 0x0000612C
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00007F44 File Offset: 0x00006144
		public Vector2 point
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00007F50 File Offset: 0x00006150
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00007F68 File Offset: 0x00006168
		public Vector2 normal
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00007F74 File Offset: 0x00006174
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00007F8C File Offset: 0x0000618C
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007F98 File Offset: 0x00006198
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00007FB0 File Offset: 0x000061B0
		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
			set
			{
				this.m_Fraction = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00007FBC File Offset: 0x000061BC
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00007FE0 File Offset: 0x000061E0
		public Rigidbody2D rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00008010 File Offset: 0x00006210
		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform transform;
				if (flag)
				{
					transform = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						transform = this.collider.transform;
					}
					else
					{
						transform = null;
					}
				}
				return transform;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000805C File Offset: 0x0000625C
		public static implicit operator bool(RaycastHit2D hit)
		{
			return hit.collider != null;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000807C File Offset: 0x0000627C
		public int CompareTo(RaycastHit2D other)
		{
			bool flag = this.collider == null;
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				bool flag2 = other.collider == null;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = this.fraction.CompareTo(other.fraction);
				}
			}
			return num;
		}

		// Token: 0x04000090 RID: 144
		[NativeName("centroid")]
		private Vector2 m_Centroid;

		// Token: 0x04000091 RID: 145
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x04000092 RID: 146
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x04000093 RID: 147
		[NativeName("distance")]
		private float m_Distance;

		// Token: 0x04000094 RID: 148
		[NativeName("fraction")]
		private float m_Fraction;

		// Token: 0x04000095 RID: 149
		[NativeName("collider")]
		private int m_Collider;
	}
}
