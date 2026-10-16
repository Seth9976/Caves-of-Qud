using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x0200001E RID: 30
	[UsedByNativeCode]
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystemDescriptor.h")]
	public class ExampleSubsystemDescriptor : IntegratedSubsystemDescriptor<ExampleSubsystem>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000306C File Offset: 0x0000126C
		public bool supportsEditorMode
		{
			get
			{
				IntPtr intPtr = ExampleSubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ExampleSubsystemDescriptor.get_supportsEditorMode_Injected(intPtr);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003090 File Offset: 0x00001290
		public bool disableBackbufferMSAA
		{
			get
			{
				IntPtr intPtr = ExampleSubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ExampleSubsystemDescriptor.get_disableBackbufferMSAA_Injected(intPtr);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000030B4 File Offset: 0x000012B4
		public bool stereoscopicBackbuffer
		{
			get
			{
				IntPtr intPtr = ExampleSubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ExampleSubsystemDescriptor.get_stereoscopicBackbuffer_Injected(intPtr);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000030D8 File Offset: 0x000012D8
		public bool usePBufferEGL
		{
			get
			{
				IntPtr intPtr = ExampleSubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ExampleSubsystemDescriptor.get_usePBufferEGL_Injected(intPtr);
			}
		}

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_supportsEditorMode_Injected(IntPtr _unity_self);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_disableBackbufferMSAA_Injected(IntPtr _unity_self);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stereoscopicBackbuffer_Injected(IntPtr _unity_self);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_usePBufferEGL_Injected(IntPtr _unity_self);

		// Token: 0x0200001F RID: 31
		internal static class BindingsMarshaller
		{
			// Token: 0x060000A6 RID: 166 RVA: 0x00002172 File Offset: 0x00000372
			public static IntPtr ConvertToNative(ExampleSubsystemDescriptor exampleSubsystemDescriptor)
			{
				return exampleSubsystemDescriptor.m_Ptr;
			}
		}
	}
}
