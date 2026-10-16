using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000502 RID: 1282
	public struct BatchPackedCullingViewID : IEquatable<BatchPackedCullingViewID>
	{
		// Token: 0x060031D7 RID: 12759 RVA: 0x0006090C File Offset: 0x0005EB0C
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0006092C File Offset: 0x0005EB2C
		public bool Equals(BatchPackedCullingViewID other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x0006094C File Offset: 0x0005EB4C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is BatchPackedCullingViewID);
			return !flag && this.Equals((BatchPackedCullingViewID)obj);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00060980 File Offset: 0x0005EB80
		public static bool operator ==(BatchPackedCullingViewID lhs, BatchPackedCullingViewID rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x0006099C File Offset: 0x0005EB9C
		public static bool operator !=(BatchPackedCullingViewID lhs, BatchPackedCullingViewID rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000609B9 File Offset: 0x0005EBB9
		public BatchPackedCullingViewID(int instanceID, int sliceIndex)
		{
			this.handle = (ulong)instanceID | (ulong)((ulong)((long)sliceIndex) << 32);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000609CC File Offset: 0x0005EBCC
		public int GetInstanceID()
		{
			return (int)(this.handle & (ulong)(-1));
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000609E8 File Offset: 0x0005EBE8
		public int GetSliceIndex()
		{
			return (int)(this.handle >> 32);
		}

		// Token: 0x04000F9F RID: 3999
		internal ulong handle;
	}
}
