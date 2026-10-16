using System;

namespace Steamworks
{
	// Token: 0x02000004 RID: 4
	public abstract class Callback
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14
		public abstract bool IsGameServer { get; }

		// Token: 0x0600000F RID: 15
		internal abstract Type GetCallbackType();

		// Token: 0x06000010 RID: 16
		internal abstract void OnRunCallback(IntPtr pvParam);

		// Token: 0x06000011 RID: 17
		internal abstract void SetUnregistered();
	}
}
