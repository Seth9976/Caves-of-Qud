using System;

namespace XRL.World
{
	// Token: 0x02000969 RID: 2409
	public class AACopyFinalEntry
	{
		// Token: 0x06006392 RID: 25490 RVA: 0x001E8B1C File Offset: 0x001E6D1C
		public AACopyFinalEntry(Guid _ID, IPart _Part)
		{
			this.ID = _ID;
			this.Part = _Part;
		}

		// Token: 0x04002ED3 RID: 11987
		public Guid ID;

		// Token: 0x04002ED4 RID: 11988
		public IPart Part;
	}
}
