using System;

namespace System.Net
{
	// Token: 0x02000564 RID: 1380
	[Flags]
	internal enum ContextFlagsPal
	{
		// Token: 0x0400181F RID: 6175
		None = 0,
		// Token: 0x04001820 RID: 6176
		Delegate = 1,
		// Token: 0x04001821 RID: 6177
		MutualAuth = 2,
		// Token: 0x04001822 RID: 6178
		ReplayDetect = 4,
		// Token: 0x04001823 RID: 6179
		SequenceDetect = 8,
		// Token: 0x04001824 RID: 6180
		Confidentiality = 16,
		// Token: 0x04001825 RID: 6181
		UseSessionKey = 32,
		// Token: 0x04001826 RID: 6182
		AllocateMemory = 256,
		// Token: 0x04001827 RID: 6183
		Connection = 2048,
		// Token: 0x04001828 RID: 6184
		InitExtendedError = 16384,
		// Token: 0x04001829 RID: 6185
		AcceptExtendedError = 32768,
		// Token: 0x0400182A RID: 6186
		InitStream = 32768,
		// Token: 0x0400182B RID: 6187
		AcceptStream = 65536,
		// Token: 0x0400182C RID: 6188
		InitIntegrity = 65536,
		// Token: 0x0400182D RID: 6189
		AcceptIntegrity = 131072,
		// Token: 0x0400182E RID: 6190
		InitManualCredValidation = 524288,
		// Token: 0x0400182F RID: 6191
		InitUseSuppliedCreds = 128,
		// Token: 0x04001830 RID: 6192
		InitIdentify = 131072,
		// Token: 0x04001831 RID: 6193
		AcceptIdentify = 524288,
		// Token: 0x04001832 RID: 6194
		ProxyBindings = 67108864,
		// Token: 0x04001833 RID: 6195
		AllowMissingBindings = 268435456,
		// Token: 0x04001834 RID: 6196
		UnverifiedTargetName = 536870912
	}
}
