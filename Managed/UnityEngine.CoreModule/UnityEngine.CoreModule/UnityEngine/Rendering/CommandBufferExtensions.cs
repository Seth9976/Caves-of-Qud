using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004EE RID: 1262
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Graphics/RenderingCommandBufferExtensions.bindings.h")]
	public static class CommandBufferExtensions
	{
		// Token: 0x0600318D RID: 12685 RVA: 0x0005F4EC File Offset: 0x0005D6EC
		[FreeFunction("RenderingCommandBufferExtensions_Bindings::Internal_SwitchIntoFastMemory")]
		private static void Internal_SwitchIntoFastMemory([NotNull] CommandBuffer cmd, ref RenderTargetIdentifier rt, FastMemoryFlags fastMemoryFlags, float residency, bool copyContents)
		{
			if (cmd == null)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(cmd);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			CommandBufferExtensions.Internal_SwitchIntoFastMemory_Injected(intPtr, ref rt, fastMemoryFlags, residency, copyContents);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x0005F528 File Offset: 0x0005D728
		[FreeFunction("RenderingCommandBufferExtensions_Bindings::Internal_SwitchOutOfFastMemory")]
		private static void Internal_SwitchOutOfFastMemory([NotNull] CommandBuffer cmd, ref RenderTargetIdentifier rt, bool copyContents)
		{
			if (cmd == null)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			IntPtr intPtr = CommandBuffer.BindingsMarshaller.ConvertToNative(cmd);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cmd, "cmd");
			}
			CommandBufferExtensions.Internal_SwitchOutOfFastMemory_Injected(intPtr, ref rt, copyContents);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0005F560 File Offset: 0x0005D760
		[NativeConditional("UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE")]
		public static void SwitchIntoFastMemory(this CommandBuffer cmd, RenderTargetIdentifier rid, FastMemoryFlags fastMemoryFlags, float residency, bool copyContents)
		{
			CommandBufferExtensions.Internal_SwitchIntoFastMemory(cmd, ref rid, fastMemoryFlags, residency, copyContents);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x0005F570 File Offset: 0x0005D770
		[NativeConditional("UNITY_XBOXONE || UNITY_GAMECORE_XBOXONE")]
		public static void SwitchOutOfFastMemory(this CommandBuffer cmd, RenderTargetIdentifier rid, bool copyContents)
		{
			CommandBufferExtensions.Internal_SwitchOutOfFastMemory(cmd, ref rid, copyContents);
		}

		// Token: 0x06003191 RID: 12689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SwitchIntoFastMemory_Injected(IntPtr cmd, ref RenderTargetIdentifier rt, FastMemoryFlags fastMemoryFlags, float residency, bool copyContents);

		// Token: 0x06003192 RID: 12690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SwitchOutOfFastMemory_Injected(IntPtr cmd, ref RenderTargetIdentifier rt, bool copyContents);
	}
}
