using System;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000140 RID: 320
	public enum AnalysisTaskState
	{
		// Token: 0x0400068D RID: 1677
		Waiting,
		// Token: 0x0400068E RID: 1678
		ReadyForSubmission,
		// Token: 0x0400068F RID: 1679
		SubmittingToPipeline,
		// Token: 0x04000690 RID: 1680
		Running,
		// Token: 0x04000691 RID: 1681
		Completed,
		// Token: 0x04000692 RID: 1682
		Failed,
		// Token: 0x04000693 RID: 1683
		Canceled
	}
}
