using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E0 RID: 5088
	public interface IRankedMutation
	{
		// Token: 0x0600C491 RID: 50321
		int GetRank();

		// Token: 0x0600C492 RID: 50322
		int AdjustRank(int amount);
	}
}
