using System;

namespace Steamworks
{
	// Token: 0x02000006 RID: 6
	public abstract class CallResult
	{
		// Token: 0x06000020 RID: 32
		internal abstract Type GetCallbackType();

		// Token: 0x06000021 RID: 33
		internal abstract void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall);

		// Token: 0x06000022 RID: 34
		internal abstract void SetUnregistered();
	}
}
