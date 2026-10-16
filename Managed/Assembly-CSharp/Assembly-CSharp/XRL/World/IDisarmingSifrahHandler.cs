using System;

namespace XRL.World
{
	// Token: 0x02000887 RID: 2183
	public interface IDisarmingSifrahHandler
	{
		// Token: 0x06005684 RID: 22148
		void DisarmingResultSuccess(GameObject Actor, GameObject Object, bool Auto);

		// Token: 0x06005685 RID: 22149
		void DisarmingResultExceptionalSuccess(GameObject Actor, GameObject Object, bool Auto);

		// Token: 0x06005686 RID: 22150
		void DisarmingResultPartialSuccess(GameObject Actor, GameObject Object, bool Auto);

		// Token: 0x06005687 RID: 22151
		void DisarmingResultFailure(GameObject Actor, GameObject Object, bool Auto);

		// Token: 0x06005688 RID: 22152
		void DisarmingResultCriticalFailure(GameObject Actor, GameObject Object, bool Auto);
	}
}
