using System;

namespace NAudio
{
	// Token: 0x02000175 RID: 373
	public enum MmResult
	{
		// Token: 0x040008C8 RID: 2248
		NoError,
		// Token: 0x040008C9 RID: 2249
		UnspecifiedError,
		// Token: 0x040008CA RID: 2250
		BadDeviceId,
		// Token: 0x040008CB RID: 2251
		NotEnabled,
		// Token: 0x040008CC RID: 2252
		AlreadyAllocated,
		// Token: 0x040008CD RID: 2253
		InvalidHandle,
		// Token: 0x040008CE RID: 2254
		NoDriver,
		// Token: 0x040008CF RID: 2255
		MemoryAllocationError,
		// Token: 0x040008D0 RID: 2256
		NotSupported,
		// Token: 0x040008D1 RID: 2257
		BadErrorNumber,
		// Token: 0x040008D2 RID: 2258
		InvalidFlag,
		// Token: 0x040008D3 RID: 2259
		InvalidParameter,
		// Token: 0x040008D4 RID: 2260
		HandleBusy,
		// Token: 0x040008D5 RID: 2261
		InvalidAlias,
		// Token: 0x040008D6 RID: 2262
		BadRegistryDatabase,
		// Token: 0x040008D7 RID: 2263
		RegistryKeyNotFound,
		// Token: 0x040008D8 RID: 2264
		RegistryReadError,
		// Token: 0x040008D9 RID: 2265
		RegistryWriteError,
		// Token: 0x040008DA RID: 2266
		RegistryDeleteError,
		// Token: 0x040008DB RID: 2267
		RegistryValueNotFound,
		// Token: 0x040008DC RID: 2268
		NoDriverCallback,
		// Token: 0x040008DD RID: 2269
		MoreData,
		// Token: 0x040008DE RID: 2270
		WaveBadFormat = 32,
		// Token: 0x040008DF RID: 2271
		WaveStillPlaying,
		// Token: 0x040008E0 RID: 2272
		WaveHeaderUnprepared,
		// Token: 0x040008E1 RID: 2273
		WaveSync,
		// Token: 0x040008E2 RID: 2274
		AcmNotPossible = 512,
		// Token: 0x040008E3 RID: 2275
		AcmBusy,
		// Token: 0x040008E4 RID: 2276
		AcmHeaderUnprepared,
		// Token: 0x040008E5 RID: 2277
		AcmCancelled,
		// Token: 0x040008E6 RID: 2278
		MixerInvalidLine = 1024,
		// Token: 0x040008E7 RID: 2279
		MixerInvalidControl,
		// Token: 0x040008E8 RID: 2280
		MixerInvalidValue
	}
}
