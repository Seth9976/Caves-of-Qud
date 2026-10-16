using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal static class SubsystemDescriptorBindings
	{
		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create(IntPtr descriptorPtr);

		// Token: 0x0600001E RID: 30 RVA: 0x000021FC File Offset: 0x000003FC
		public static string GetId(IntPtr descriptorPtr)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SubsystemDescriptorBindings.GetId_Injected(descriptorPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetId_Injected(IntPtr descriptorPtr, out ManagedSpanWrapper ret);
	}
}
