using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000019 RID: 25
	public abstract class SubsystemWithProvider<TSubsystem, TSubsystemDescriptor, TProvider> : SubsystemWithProvider where TSubsystem : SubsystemWithProvider, new() where TSubsystemDescriptor : SubsystemDescriptorWithProvider where TProvider : SubsystemProvider<TSubsystem>
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002F38 File Offset: 0x00001138
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002F40 File Offset: 0x00001140
		public TSubsystemDescriptor subsystemDescriptor { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002F49 File Offset: 0x00001149
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002F51 File Offset: 0x00001151
		protected internal TProvider provider { get; private set; }

		// Token: 0x0600008E RID: 142 RVA: 0x00002F5A File Offset: 0x0000115A
		protected virtual void OnCreate()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002F5D File Offset: 0x0000115D
		protected override void OnStart()
		{
			this.provider.Start();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002F70 File Offset: 0x00001170
		protected override void OnStop()
		{
			this.provider.Stop();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002F83 File Offset: 0x00001183
		protected override void OnDestroy()
		{
			this.provider.Destroy();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002F96 File Offset: 0x00001196
		internal sealed override void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider provider)
		{
			base.providerBase = provider;
			this.provider = (TProvider)((object)provider);
			this.subsystemDescriptor = (TSubsystemDescriptor)((object)descriptor);
			this.OnCreate();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002FC2 File Offset: 0x000011C2
		internal sealed override SubsystemDescriptorWithProvider descriptor
		{
			get
			{
				return this.subsystemDescriptor;
			}
		}
	}
}
