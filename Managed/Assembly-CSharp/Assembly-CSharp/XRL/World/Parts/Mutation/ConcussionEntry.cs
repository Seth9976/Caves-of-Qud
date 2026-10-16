using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013FA RID: 5114
	public class ConcussionEntry
	{
		// Token: 0x0600C647 RID: 50759 RVA: 0x00483038 File Offset: 0x00481238
		public ConcussionEntry(Cell C, int D, string Dir)
		{
			this.C = C;
			this.Distance = D;
			this.Direction = Dir;
		}

		// Token: 0x04004E30 RID: 20016
		public Cell C;

		// Token: 0x04004E31 RID: 20017
		public int Distance;

		// Token: 0x04004E32 RID: 20018
		public string Direction;
	}
}
