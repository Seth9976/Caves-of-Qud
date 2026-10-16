using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[UsedByNativeCode("SubsystemDescriptor")]
	[NativeHeader("Modules/Subsystems/SubsystemDescriptor.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystemDescriptor<TSubsystem> : IntegratedSubsystemDescriptor where TSubsystem : IntegratedSubsystem
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000218C File Offset: 0x0000038C
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021AC File Offset: 0x000003AC
		public TSubsystem Create()
		{
			IntPtr intPtr = SubsystemDescriptorBindings.Create(this.m_Ptr);
			TSubsystem tsubsystem = (TSubsystem)((object)SubsystemManager.GetIntegratedSubsystemByPtr(intPtr));
			bool flag = tsubsystem != null;
			if (flag)
			{
				tsubsystem.m_SubsystemDescriptor = this;
			}
			return tsubsystem;
		}
	}
}
