using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::System", StaticAccessorType.DoubleColon)]
	public static class System
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		internal static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000002 RID: 2
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Update();

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		[NativeThrows]
		internal static void SetDebugFlag(Guid guid, bool enabled)
		{
			UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagInteger(guid.ToByteArray(), enabled ? 1L : 0L);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		[NativeThrows]
		internal static void SetDebugFlagInteger(Guid guid, long value)
		{
			UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagInteger(guid.ToByteArray(), value);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002079 File Offset: 0x00000279
		[NativeThrows]
		internal static void SetDebugFlagDouble(Guid guid, double value)
		{
			UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagDouble(guid.ToByteArray(), value);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000208C File Offset: 0x0000028C
		[NativeThrows]
		private unsafe static void SetDebugFlagInteger(byte[] guid, long value)
		{
			Span<byte> span = new Span<byte>(guid);
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagInteger_Injected(ref managedSpanWrapper, value);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		[NativeThrows]
		private unsafe static void SetDebugFlagDouble(byte[] guid, double value)
		{
			Span<byte> span = new Span<byte>(guid);
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				UnityEngine.Rendering.VirtualTexturing.System.SetDebugFlagDouble_Injected(ref managedSpanWrapper, value);
			}
		}

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDebugFlagInteger_Injected(ref ManagedSpanWrapper guid, long value);

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDebugFlagDouble_Injected(ref ManagedSpanWrapper guid, double value);

		// Token: 0x04000001 RID: 1
		public const int AllMips = 2147483647;
	}
}
