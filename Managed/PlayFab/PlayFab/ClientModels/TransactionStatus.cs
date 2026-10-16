using System;

namespace PlayFab.ClientModels
{
	// Token: 0x020002DE RID: 734
	public enum TransactionStatus
	{
		// Token: 0x04000EF8 RID: 3832
		CreateCart,
		// Token: 0x04000EF9 RID: 3833
		Init,
		// Token: 0x04000EFA RID: 3834
		Approved,
		// Token: 0x04000EFB RID: 3835
		Succeeded,
		// Token: 0x04000EFC RID: 3836
		FailedByProvider,
		// Token: 0x04000EFD RID: 3837
		DisputePending,
		// Token: 0x04000EFE RID: 3838
		RefundPending,
		// Token: 0x04000EFF RID: 3839
		Refunded,
		// Token: 0x04000F00 RID: 3840
		RefundFailed,
		// Token: 0x04000F01 RID: 3841
		ChargedBack,
		// Token: 0x04000F02 RID: 3842
		FailedByUber,
		// Token: 0x04000F03 RID: 3843
		FailedByPlayFab,
		// Token: 0x04000F04 RID: 3844
		Revoked,
		// Token: 0x04000F05 RID: 3845
		TradePending,
		// Token: 0x04000F06 RID: 3846
		Traded,
		// Token: 0x04000F07 RID: 3847
		Upgraded,
		// Token: 0x04000F08 RID: 3848
		StackPending,
		// Token: 0x04000F09 RID: 3849
		Stacked,
		// Token: 0x04000F0A RID: 3850
		Other,
		// Token: 0x04000F0B RID: 3851
		Failed
	}
}
