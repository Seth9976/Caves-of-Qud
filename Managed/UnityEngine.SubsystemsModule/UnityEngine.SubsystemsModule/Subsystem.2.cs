using System;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[Obsolete("Use SubsystemWithProvider<> instead.", false)]
	public abstract class Subsystem<TSubsystemDescriptor> : Subsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000224B File Offset: 0x0000044B
		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_SubsystemDescriptor);
			}
		}
	}
}
