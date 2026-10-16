using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A8 RID: 1448
	public class QudGenotypeModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003DCC RID: 15820 RVA: 0x0012F86C File Offset: 0x0012DA6C
		public QudGenotypeModuleData(string id)
		{
			this.Genotype = id;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x0012F87B File Offset: 0x0012DA7B
		[Obsolete]
		public GenotypeEntry info
		{
			get
			{
				return GenotypeFactory.RequireGenotypeEntry(this.Genotype);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x0012F888 File Offset: 0x0012DA88
		public GenotypeEntry Entry
		{
			get
			{
				return GenotypeFactory.RequireGenotypeEntry(this.Genotype);
			}
		}

		// Token: 0x04001EAD RID: 7853
		public string Genotype;
	}
}
