using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[Obsolete("Use SubsystemDescriptorWithProvider instead.", false)]
	public abstract class SubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002261 File Offset: 0x00000461
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002269 File Offset: 0x00000469
		public string id { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002272 File Offset: 0x00000472
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000227A File Offset: 0x0000047A
		public Type subsystemImplementationType { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002283 File Offset: 0x00000483
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x06000033 RID: 51
		internal abstract ISubsystem CreateImpl();
	}
}
