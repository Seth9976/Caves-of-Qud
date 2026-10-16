using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000408 RID: 1032
	internal class PdbSynchronizationPoint
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x000463A1 File Offset: 0x000445A1
		internal PdbSynchronizationPoint(BitAccess bits)
		{
			bits.ReadUInt32(out this.synchronizeOffset);
			bits.ReadUInt32(out this.continuationMethodToken);
			bits.ReadUInt32(out this.continuationOffset);
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000463CD File Offset: 0x000445CD
		public uint SynchronizeOffset
		{
			get
			{
				return this.synchronizeOffset;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000463D5 File Offset: 0x000445D5
		public uint ContinuationOffset
		{
			get
			{
				return this.continuationOffset;
			}
		}

		// Token: 0x04000F64 RID: 3940
		internal uint synchronizeOffset;

		// Token: 0x04000F65 RID: 3941
		internal uint continuationMethodToken;

		// Token: 0x04000F66 RID: 3942
		internal uint continuationOffset;
	}
}
