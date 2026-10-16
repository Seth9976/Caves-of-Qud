using System;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[Obsolete("Use SubsystemWithProvider instead.", false)]
	public abstract class Subsystem : ISubsystem
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38
		public abstract bool running { get; }

		// Token: 0x06000027 RID: 39
		public abstract void Start();

		// Token: 0x06000028 RID: 40
		public abstract void Stop();

		// Token: 0x06000029 RID: 41 RVA: 0x0000222C File Offset: 0x0000042C
		public void Destroy()
		{
			bool flag = SubsystemManager.RemoveDeprecatedSubsystem(this);
			if (flag)
			{
				this.OnDestroy();
			}
		}

		// Token: 0x0600002A RID: 42
		protected abstract void OnDestroy();

		// Token: 0x04000004 RID: 4
		internal ISubsystemDescriptor m_SubsystemDescriptor;
	}
}
