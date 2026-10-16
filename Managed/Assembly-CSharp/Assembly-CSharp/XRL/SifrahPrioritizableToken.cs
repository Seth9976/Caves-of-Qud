using System;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x02000518 RID: 1304
	public abstract class SifrahPrioritizableToken : SifrahToken, SifrahPrioritizable
	{
		// Token: 0x060037EF RID: 14319
		public abstract int GetPriority();

		// Token: 0x060037F0 RID: 14320
		public abstract int GetTiebreakerPriority();
	}
}
