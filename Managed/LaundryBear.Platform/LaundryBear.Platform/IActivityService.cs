using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200000C RID: 12
	public interface IActivityService
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001D RID: 29
		// (remove) Token: 0x0600001E RID: 30
		event ActivityRequestEventHandler ActivityRequestEvent;

		// Token: 0x0600001F RID: 31
		void StartActivity(IUser user, string activityID);

		// Token: 0x06000020 RID: 32
		void EndActivity(IUser user, string activityID, ActivityEndType endType);

		// Token: 0x06000021 RID: 33
		void ResumeActivity(IUser user, string activityID);

		// Token: 0x06000022 RID: 34
		void ResumeActivity(IUser user, string activityID, string[] subactivitiesInProgress, string[] subactivitiesCompleted);

		// Token: 0x06000023 RID: 35
		void TerminateActivity(IUser user, string activityID);

		// Token: 0x06000024 RID: 36
		void UpdateActivityAvailability(IUser user, string[] activityID, ActivityAvailability availability);
	}
}
