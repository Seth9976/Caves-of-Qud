using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x0200001C RID: 28
	[UsedByNativeCode]
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystem.h")]
	public class ExampleSubsystem : IntegratedSubsystem<ExampleSubsystemDescriptor>
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000301C File Offset: 0x0000121C
		public void PrintExample()
		{
			IntPtr intPtr = ExampleSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ExampleSubsystem.PrintExample_Injected(intPtr);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003040 File Offset: 0x00001240
		public bool GetBool()
		{
			IntPtr intPtr = ExampleSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ExampleSubsystem.GetBool_Injected(intPtr);
		}

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrintExample_Injected(IntPtr _unity_self);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBool_Injected(IntPtr _unity_self);

		// Token: 0x0200001D RID: 29
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600009C RID: 156 RVA: 0x0000213F File Offset: 0x0000033F
			public static IntPtr ConvertToNative(ExampleSubsystem exampleSubsystem)
			{
				return exampleSubsystem.m_Ptr;
			}
		}
	}
}
