using System;

namespace QupKit
{
	// Token: 0x020003AE RID: 942
	public struct Vector2i
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x000B252A File Offset: 0x000B072A
		public Vector2i(int[] xy)
		{
			this.x = xy[0];
			this.y = xy[1];
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000B253E File Offset: 0x000B073E
		public Vector2i(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000B254E File Offset: 0x000B074E
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000B255D File Offset: 0x000B075D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (Vector2i)obj == this;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000B2592 File Offset: 0x000B0792
		public static bool operator ==(Vector2i c1, Vector2i c2)
		{
			return c1.x == c2.x && c1.y == c2.y;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000B25B2 File Offset: 0x000B07B2
		public static bool operator !=(Vector2i c1, Vector2i c2)
		{
			return c1.x != c2.x || c1.y != c2.y;
		}

		// Token: 0x040011B0 RID: 4528
		public int x;

		// Token: 0x040011B1 RID: 4529
		public int y;
	}
}
