using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000017 RID: 23
	public class SubsystemProxy<TSubsystem, TProvider> where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002E39 File Offset: 0x00001039
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002E41 File Offset: 0x00001041
		public TProvider provider { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002E4A File Offset: 0x0000104A
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002E5C File Offset: 0x0000105C
		public bool running
		{
			get
			{
				return this.provider.running;
			}
			set
			{
				this.provider.m_Running = value;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E6F File Offset: 0x0000106F
		internal SubsystemProxy(TProvider provider)
		{
			this.provider = provider;
		}
	}
}
