using System;

namespace XRL
{
	// Token: 0x02000503 RID: 1283
	[Serializable]
	public class GenotypeReputation
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x0010B136 File Offset: 0x00109336
		public void MergeWith(GenotypeReputation newReputation)
		{
			if (newReputation.Value != -999)
			{
				this.Value = newReputation.Value;
			}
		}

		// Token: 0x04001B79 RID: 7033
		public string With = "";

		// Token: 0x04001B7A RID: 7034
		public int Value;
	}
}
