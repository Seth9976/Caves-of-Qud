using System;

namespace XRL.World
{
	// Token: 0x020008B9 RID: 2233
	public interface IRepairSifrahHandler
	{
		// Token: 0x0600573A RID: 22330
		void RepairResultSuccess(GameObject who, GameObject obj);

		// Token: 0x0600573B RID: 22331
		void RepairResultExceptionalSuccess(GameObject who, GameObject obj);

		// Token: 0x0600573C RID: 22332
		void RepairResultPartialSuccess(GameObject who, GameObject obj);

		// Token: 0x0600573D RID: 22333
		void RepairResultFailure(GameObject who, GameObject obj);

		// Token: 0x0600573E RID: 22334
		void RepairResultCriticalFailure(GameObject who, GameObject obj);
	}
}
