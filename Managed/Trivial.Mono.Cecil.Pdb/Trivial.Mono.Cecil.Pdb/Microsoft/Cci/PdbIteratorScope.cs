using System;

namespace Microsoft.Cci
{
	// Token: 0x02000015 RID: 21
	internal sealed class PdbIteratorScope : ILocalScope
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003B67 File Offset: 0x00001D67
		public uint Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003B6F File Offset: 0x00001D6F
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003B77 File Offset: 0x00001D77
		internal PdbIteratorScope(uint offset, uint length)
		{
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x0400001E RID: 30
		private uint offset;

		// Token: 0x0400001F RID: 31
		private uint length;
	}
}
