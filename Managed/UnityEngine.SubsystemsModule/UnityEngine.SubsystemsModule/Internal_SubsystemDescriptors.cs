using System;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	internal static class Internal_SubsystemDescriptors
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002304 File Offset: 0x00000504
		[RequiredByNativeCode]
		internal static void Internal_AddDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDeprecatedDescriptor(descriptor);
		}
	}
}
