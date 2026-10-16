using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000411 RID: 1041
	internal sealed class PdbIteratorScope : ILocalScope
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00046833 File Offset: 0x00044A33
		internal PdbIteratorScope(uint offset, uint length)
		{
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00046849 File Offset: 0x00044A49
		public uint Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00046851 File Offset: 0x00044A51
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x04000F8F RID: 3983
		private uint offset;

		// Token: 0x04000F90 RID: 3984
		private uint length;
	}
}
