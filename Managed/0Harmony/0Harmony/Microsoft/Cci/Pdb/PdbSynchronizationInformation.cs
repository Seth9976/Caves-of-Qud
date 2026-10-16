using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000407 RID: 1031
	internal class PdbSynchronizationInformation
	{
		// Token: 0x060015DF RID: 5599 RVA: 0x00046340 File Offset: 0x00044540
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

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00046399 File Offset: 0x00044599
		public uint GeneratedCatchHandlerOffset
		{
			get
			{
				return this.generatedCatchHandlerIlOffset;
			}
		}

		// Token: 0x04000F61 RID: 3937
		internal uint kickoffMethodToken;

		// Token: 0x04000F62 RID: 3938
		internal uint generatedCatchHandlerIlOffset;

		// Token: 0x04000F63 RID: 3939
		internal PdbSynchronizationPoint[] synchronizationPoints;
	}
}
