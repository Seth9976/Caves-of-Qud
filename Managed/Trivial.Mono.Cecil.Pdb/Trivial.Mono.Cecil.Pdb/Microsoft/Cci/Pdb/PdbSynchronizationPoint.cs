using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000CA RID: 202
	internal class PdbSynchronizationPoint
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006AC5 File Offset: 0x00004CC5
		public uint SynchronizeOffset
		{
			get
			{
				return this.synchronizeOffset;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006ACD File Offset: 0x00004CCD
		public uint ContinuationOffset
		{
			get
			{
				return this.continuationOffset;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006AD5 File Offset: 0x00004CD5
		internal PdbSynchronizationPoint(BitAccess bits)
		{
			bits.ReadUInt32(out this.synchronizeOffset);
			bits.ReadUInt32(out this.continuationMethodToken);
			bits.ReadUInt32(out this.continuationOffset);
		}

		// Token: 0x04000398 RID: 920
		internal uint synchronizeOffset;

		// Token: 0x04000399 RID: 921
		internal uint continuationMethodToken;

		// Token: 0x0400039A RID: 922
		internal uint continuationOffset;
	}
}
