using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[UsedByNativeCode]
	[NativeHeader("Modules/Subsystems/Subsystem.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class IntegratedSubsystem : ISubsystem
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal void SetHandle([Unmarshalled] IntegratedSubsystem subsystem)
		{
			IntPtr intPtr = IntegratedSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntegratedSubsystem.SetHandle_Injected(intPtr, subsystem);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		public void Start()
		{
			IntPtr intPtr = IntegratedSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntegratedSubsystem.Start_Injected(intPtr);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002098 File Offset: 0x00000298
		public void Stop()
		{
			IntPtr intPtr = IntegratedSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntegratedSubsystem.Stop_Injected(intPtr);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020BC File Offset: 0x000002BC
		public void Destroy()
		{
			IntPtr ptr = this.m_Ptr;
			SubsystemManager.RemoveIntegratedSubsystemByPtr(this.m_Ptr);
			SubsystemBindings.DestroySubsystem(ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020EF File Offset: 0x000002EF
		public bool running
		{
			get
			{
				return this.valid && this.IsRunning();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002102 File Offset: 0x00000302
		internal bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		internal bool IsRunning()
		{
			IntPtr intPtr = IntegratedSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return IntegratedSubsystem.IsRunning_Injected(intPtr);
		}

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHandle_Injected(IntPtr _unity_self, IntegratedSubsystem subsystem);

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Start_Injected(IntPtr _unity_self);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRunning_Injected(IntPtr _unity_self);

		// Token: 0x04000001 RID: 1
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x04000002 RID: 2
		internal ISubsystemDescriptor m_SubsystemDescriptor;

		// Token: 0x02000003 RID: 3
		internal static class BindingsMarshaller
		{
			// Token: 0x0600000D RID: 13 RVA: 0x0000213F File Offset: 0x0000033F
			public static IntPtr ConvertToNative(IntegratedSubsystem integratedSubsystem)
			{
				return integratedSubsystem.m_Ptr;
			}
		}
	}
}
