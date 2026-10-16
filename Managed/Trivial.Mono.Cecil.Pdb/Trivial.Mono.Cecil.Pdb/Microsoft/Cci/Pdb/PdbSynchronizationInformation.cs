using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C9 RID: 201
	internal class PdbSynchronizationInformation
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006A62 File Offset: 0x00004C62
		public uint GeneratedCatchHandlerOffset
		{
			get
			{
				return this.generatedCatchHandlerIlOffset;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006A6C File Offset: 0x00004C6C
		internal PdbSynchronizationInformation(BitAccess bits)
		{
			bits.ReadUInt32(out this.kickoffMethodToken);
			bits.ReadUInt32(out this.generatedCatchHandlerIlOffset);
			uint num;
			bits.ReadUInt32(out num);
			this.synchronizationPoints = new PdbSynchronizationPoint[num];
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.synchronizationPoints[(int)num2] = new PdbSynchronizationPoint(bits);
			}
		}

		// Token: 0x04000395 RID: 917
		internal uint kickoffMethodToken;

		// Token: 0x04000396 RID: 918
		internal uint generatedCatchHandlerIlOffset;

		// Token: 0x04000397 RID: 919
		internal PdbSynchronizationPoint[] synchronizationPoints;
	}
}
