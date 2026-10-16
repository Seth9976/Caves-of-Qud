using System;

namespace XRL
{
	// Token: 0x02000509 RID: 1289
	[Serializable]
	public class SubtypeReputation
	{
		// Token: 0x06003750 RID: 14160 RVA: 0x0010C4C3 File Offset: 0x0010A6C3
		public void MergeWith(SubtypeReputation newReputation)
		{
			if (newReputation.Value != -999)
			{
				this.Value = newReputation.Value;
			}
		}

		// Token: 0x04001BA6 RID: 7078
		public string With = "";

		// Token: 0x04001BA7 RID: 7079
		public int Value;
	}
}
