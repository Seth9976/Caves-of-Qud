using System;

namespace Genkit
{
	// Token: 0x0200038F RID: 911
	public struct Point3D
	{
		// Token: 0x06002244 RID: 8772 RVA: 0x000AC018 File Offset: 0x000AA218
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.x.ToString(),
				",",
				this.y.ToString(),
				",",
				this.z.ToString()
			});
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000AC06A File Offset: 0x000AA26A
		public Point3D(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000AC081 File Offset: 0x000AA281
		public override int GetHashCode()
		{
			return this.x ^ this.y ^ this.z;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000AC097 File Offset: 0x000AA297
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (Point3D)obj == this;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000AC0CC File Offset: 0x000AA2CC
		public static bool operator ==(Point3D c1, Point3D c2)
		{
			return c1.x == c2.x && c1.y == c2.y && c1.z == c2.z;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000AC0FA File Offset: 0x000AA2FA
		public static bool operator !=(Point3D c1, Point3D c2)
		{
			return c1.x != c2.x || c1.y != c2.y || c1.z != c2.z;
		}

		// Token: 0x0400112E RID: 4398
		public static readonly Point3D zero = new Point3D(0, 0, 0);

		// Token: 0x0400112F RID: 4399
		public static readonly Point3D invalid = new Point3D(int.MinValue, int.MaxValue, int.MinValue);

		// Token: 0x04001130 RID: 4400
		public int x;

		// Token: 0x04001131 RID: 4401
		public int y;

		// Token: 0x04001132 RID: 4402
		public int z;
	}
}
