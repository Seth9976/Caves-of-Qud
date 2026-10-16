using System;

namespace XRL.World
{
	// Token: 0x0200088B RID: 2187
	public interface IHackingSifrahHandler
	{
		// Token: 0x060056A2 RID: 22178
		void HackingResultSuccess(GameObject Actor, GameObject Object, HackingSifrah Game);

		// Token: 0x060056A3 RID: 22179
		void HackingResultExceptionalSuccess(GameObject Actor, GameObject Object, HackingSifrah Game);

		// Token: 0x060056A4 RID: 22180
		void HackingResultPartialSuccess(GameObject Actor, GameObject Object, HackingSifrah Game);

		// Token: 0x060056A5 RID: 22181
		void HackingResultFailure(GameObject Actor, GameObject Object, HackingSifrah Game);

		// Token: 0x060056A6 RID: 22182
		void HackingResultCriticalFailure(GameObject Actor, GameObject Object, HackingSifrah Game);
	}
}
