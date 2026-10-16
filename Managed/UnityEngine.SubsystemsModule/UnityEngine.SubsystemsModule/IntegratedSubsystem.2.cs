using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[UsedByNativeCode("Subsystem_TSubsystemDescriptor")]
	public class IntegratedSubsystem<TSubsystemDescriptor> : IntegratedSubsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002147 File Offset: 0x00000347
		public TSubsystemDescriptor subsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_SubsystemDescriptor);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002154 File Offset: 0x00000354
		[Obsolete("The property 'SubsystemDescriptor' is deprecated. Use `subsystemDescriptor` instead. UnityUpgradeable -> subsystemDescriptor", false)]
		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return this.subsystemDescriptor;
			}
		}
	}
}
