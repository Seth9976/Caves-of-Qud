using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000686 RID: 1670
	internal struct BMPAlloc
	{
		// Token: 0x0600308A RID: 12426 RVA: 0x000B7FFC File Offset: 0x000B61FC
		public bool Equals(BMPAlloc other)
		{
			return this.page == other.page && this.pageLine == other.pageLine && this.bitIndex == other.bitIndex;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000B803C File Offset: 0x000B623C
		public bool IsValid()
		{
			return this.page >= 0;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000B805C File Offset: 0x000B625C
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", this.page, this.pageLine, this.bitIndex);
		}

		// Token: 0x040017E4 RID: 6116
		public static readonly BMPAlloc Invalid = new BMPAlloc
		{
			page = -1
		};

		// Token: 0x040017E5 RID: 6117
		public int page;

		// Token: 0x040017E6 RID: 6118
		public ushort pageLine;

		// Token: 0x040017E7 RID: 6119
		public byte bitIndex;

		// Token: 0x040017E8 RID: 6120
		public OwnedState ownedState;
	}
}
