using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000018 RID: 24
	public abstract class SubsystemWithProvider : ISubsystem
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002E80 File Offset: 0x00001080
		public void Start()
		{
			bool running = this.running;
			if (!running)
			{
				this.OnStart();
				this.providerBase.m_Running = true;
				this.running = true;
			}
		}

		// Token: 0x0600007E RID: 126
		protected abstract void OnStart();

		// Token: 0x0600007F RID: 127 RVA: 0x00002EB8 File Offset: 0x000010B8
		public void Stop()
		{
			bool flag = !this.running;
			if (!flag)
			{
				this.OnStop();
				this.providerBase.m_Running = false;
				this.running = false;
			}
		}

		// Token: 0x06000080 RID: 128
		protected abstract void OnStop();

		// Token: 0x06000081 RID: 129 RVA: 0x00002EF0 File Offset: 0x000010F0
		public void Destroy()
		{
			this.Stop();
			bool flag = SubsystemManager.RemoveStandaloneSubsystem(this);
			if (flag)
			{
				this.OnDestroy();
			}
		}

		// Token: 0x06000082 RID: 130
		protected abstract void OnDestroy();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002F16 File Offset: 0x00001116
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002F1E File Offset: 0x0000111E
		public bool running { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002F27 File Offset: 0x00001127
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002F2F File Offset: 0x0000112F
		internal SubsystemProvider providerBase { get; set; }

		// Token: 0x06000087 RID: 135
		internal abstract void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider subsystemProvider);

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000088 RID: 136
		internal abstract SubsystemDescriptorWithProvider descriptor { get; }
	}
}
