using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000016 RID: 22
	public abstract class SubsystemProvider<TSubsystem> : SubsystemProvider where TSubsystem : SubsystemWithProvider, new()
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002E2D File Offset: 0x0000102D
		protected internal virtual bool TryInitialize()
		{
			return true;
		}

		// Token: 0x06000074 RID: 116
		public abstract void Start();

		// Token: 0x06000075 RID: 117
		public abstract void Stop();

		// Token: 0x06000076 RID: 118
		public abstract void Destroy();
	}
}
