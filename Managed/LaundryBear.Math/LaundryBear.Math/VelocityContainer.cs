using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public struct VelocityContainer
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002FD9 File Offset: 0x000011D9
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002FE1 File Offset: 0x000011E1
		public Vector3 Direction
		{
			get
			{
				return this.m_direction;
			}
			set
			{
				if (Mathf.Abs(value.sqrMagnitude - 1f) > 1E-05f)
				{
					throw new ArgumentException("The Direction member of a Velocity type must be normalized");
				}
				this.m_direction = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000300E File Offset: 0x0000120E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003016 File Offset: 0x00001216
		public float Speed
		{
			get
			{
				return this.m_speed;
			}
			set
			{
				if (0f > value)
				{
					Debug.LogWarning("The Speed member of a Velocity type must be 0 or greater.  Clamping value to 0.");
				}
				this.m_speed = Mathf.Clamp(value, 0f, float.MaxValue);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003040 File Offset: 0x00001240
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003053 File Offset: 0x00001253
		public Vector3 Velocity
		{
			get
			{
				return this.m_direction * this.m_speed;
			}
			set
			{
				Debug.LogWarning("Setting velocity vector directly. This is an expensive operation.");
				if (value.sqrMagnitude > 1E-05f)
				{
					this.Direction = value.normalized;
				}
				this.Speed = value.magnitude;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003087 File Offset: 0x00001287
		public static implicit operator Vector3(VelocityContainer value)
		{
			return value.Velocity;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003090 File Offset: 0x00001290
		public static Vector3 operator +(VelocityContainer valueA, VelocityContainer valueB)
		{
			return valueA.Velocity + valueB.Velocity;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030A5 File Offset: 0x000012A5
		public static Vector3 operator -(VelocityContainer value)
		{
			return -value.Velocity;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030B3 File Offset: 0x000012B3
		public static VelocityContainer operator *(VelocityContainer value, float scalar)
		{
			value.Speed *= scalar;
			return value;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030C5 File Offset: 0x000012C5
		public static VelocityContainer operator *(float scalar, VelocityContainer value)
		{
			value.Speed *= scalar;
			return value;
		}

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private Vector3 m_direction;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private float m_speed;
	}
}
