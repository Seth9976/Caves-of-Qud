using System;

namespace UnityEngine
{
	// Token: 0x020002D7 RID: 727
	internal struct SnapAxisFilter : IEquatable<SnapAxisFilter>
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x00045090 File Offset: 0x00043290
		public float x
		{
			get
			{
				return ((this.m_Mask & SnapAxis.X) == SnapAxis.X) ? 1f : 0f;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000450BC File Offset: 0x000432BC
		public float y
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Y) == SnapAxis.Y) ? 1f : 0f;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000450E8 File Offset: 0x000432E8
		public float z
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Z) == SnapAxis.Z) ? 1f : 0f;
			}
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00045114 File Offset: 0x00043314
		public SnapAxisFilter(Vector3 v)
		{
			this.m_Mask = SnapAxis.None;
			float num = 1E-06f;
			bool flag = Mathf.Abs(v.x) > num;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = Mathf.Abs(v.y) > num;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = Mathf.Abs(v.z) > num;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00045190 File Offset: 0x00043390
		public SnapAxisFilter(SnapAxis axis)
		{
			this.m_Mask = SnapAxis.None;
			bool flag = (axis & SnapAxis.X) == SnapAxis.X;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = (axis & SnapAxis.Y) == SnapAxis.Y;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = (axis & SnapAxis.Z) == SnapAxis.Z;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000451F0 File Offset: 0x000433F0
		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}", this.x, this.y, this.z);
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00045230 File Offset: 0x00043430
		public int active
		{
			get
			{
				int num = 0;
				bool flag = (this.m_Mask & SnapAxis.X) > SnapAxis.None;
				if (flag)
				{
					num++;
				}
				bool flag2 = (this.m_Mask & SnapAxis.Y) > SnapAxis.None;
				if (flag2)
				{
					num++;
				}
				bool flag3 = (this.m_Mask & SnapAxis.Z) > SnapAxis.None;
				if (flag3)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00045280 File Offset: 0x00043480
		public static implicit operator Vector3(SnapAxisFilter mask)
		{
			return new Vector3(mask.x, mask.y, mask.z);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000452AC File Offset: 0x000434AC
		public static explicit operator SnapAxisFilter(Vector3 v)
		{
			return new SnapAxisFilter(v);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000452C4 File Offset: 0x000434C4
		public static explicit operator SnapAxis(SnapAxisFilter mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000452DC File Offset: 0x000434DC
		public static SnapAxisFilter operator |(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask | right.m_Mask);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00045300 File Offset: 0x00043500
		public static SnapAxisFilter operator &(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask & right.m_Mask);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00045324 File Offset: 0x00043524
		public static SnapAxisFilter operator ^(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask ^ right.m_Mask);
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00045348 File Offset: 0x00043548
		public static SnapAxisFilter operator ~(SnapAxisFilter left)
		{
			return new SnapAxisFilter(~left.m_Mask);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00045368 File Offset: 0x00043568
		public static Vector3 operator *(SnapAxisFilter mask, float value)
		{
			return new Vector3(mask.x * value, mask.y * value, mask.z * value);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0004539C File Offset: 0x0004359C
		public static Vector3 operator *(SnapAxisFilter mask, Vector3 right)
		{
			return new Vector3(mask.x * right.x, mask.y * right.y, mask.z * right.z);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000453E0 File Offset: 0x000435E0
		public static Vector3 operator *(Quaternion rotation, SnapAxisFilter mask)
		{
			int active = mask.active;
			bool flag = active > 2;
			Vector3 vector;
			if (flag)
			{
				vector = mask;
			}
			else
			{
				Vector3 vector2 = rotation * mask;
				vector2 = new Vector3(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y), Mathf.Abs(vector2.z));
				bool flag2 = active > 1;
				if (flag2)
				{
					vector = new Vector3((float)((vector2.x > vector2.y || vector2.x > vector2.z) ? 1 : 0), (float)((vector2.y > vector2.x || vector2.y > vector2.z) ? 1 : 0), (float)((vector2.z > vector2.x || vector2.z > vector2.y) ? 1 : 0));
				}
				else
				{
					vector = new Vector3((float)((vector2.x > vector2.y && vector2.x > vector2.z) ? 1 : 0), (float)((vector2.y > vector2.z && vector2.y > vector2.x) ? 1 : 0), (float)((vector2.z > vector2.x && vector2.z > vector2.y) ? 1 : 0));
				}
			}
			return vector;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00045524 File Offset: 0x00043724
		public static bool operator ==(SnapAxisFilter left, SnapAxisFilter right)
		{
			return left.m_Mask == right.m_Mask;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00045544 File Offset: 0x00043744
		public static bool operator !=(SnapAxisFilter left, SnapAxisFilter right)
		{
			return !(left == right);
		}

		// Token: 0x1700055A RID: 1370
		public float this[int i]
		{
			get
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return (float)(SnapAxis.X & (this.m_Mask >> (i & 31))) * 1f;
			}
			set
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				this.m_Mask &= (SnapAxis)(~(SnapAxis)(1 << i));
				this.m_Mask |= (SnapAxis)(((value > 0f) ? 1 : 0) << (i & 31));
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000455F4 File Offset: 0x000437F4
		public bool Equals(SnapAxisFilter other)
		{
			return this.m_Mask == other.m_Mask;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00045614 File Offset: 0x00043814
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SnapAxisFilter && this.Equals((SnapAxisFilter)obj);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0004564C File Offset: 0x0004384C
		public override int GetHashCode()
		{
			return this.m_Mask.GetHashCode();
		}

		// Token: 0x04000A2C RID: 2604
		private const SnapAxis X = SnapAxis.X;

		// Token: 0x04000A2D RID: 2605
		private const SnapAxis Y = SnapAxis.Y;

		// Token: 0x04000A2E RID: 2606
		private const SnapAxis Z = SnapAxis.Z;

		// Token: 0x04000A2F RID: 2607
		public static readonly SnapAxisFilter all = new SnapAxisFilter(SnapAxis.All);

		// Token: 0x04000A30 RID: 2608
		private SnapAxis m_Mask;
	}
}
