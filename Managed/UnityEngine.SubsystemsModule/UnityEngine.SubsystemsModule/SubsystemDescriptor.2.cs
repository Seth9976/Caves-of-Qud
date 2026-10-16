using System;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[Obsolete("Use SubsystemDescriptorWithProvider<> instead.", false)]
	public class SubsystemDescriptor<TSubsystem> : SubsystemDescriptor where TSubsystem : Subsystem
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000228B File Offset: 0x0000048B
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002298 File Offset: 0x00000498
		public TSubsystem Create()
		{
			TSubsystem tsubsystem = SubsystemManager.FindDeprecatedSubsystemByDescriptor(this) as TSubsystem;
			bool flag = tsubsystem != null;
			TSubsystem tsubsystem2;
			if (flag)
			{
				tsubsystem2 = tsubsystem;
			}
			else
			{
				tsubsystem = Activator.CreateInstance(base.subsystemImplementationType) as TSubsystem;
				tsubsystem.m_SubsystemDescriptor = this;
				SubsystemManager.AddDeprecatedSubsystem(tsubsystem);
				tsubsystem2 = tsubsystem;
			}
			return tsubsystem2;
		}
	}
}
