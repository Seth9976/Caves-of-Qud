using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Diagnostics
{
	// Token: 0x02000460 RID: 1120
	[NativeHeader("Runtime/Misc/GarbageCollectSharedAssets.h")]
	[NativeHeader("Runtime/Export/Diagnostics/DiagnosticsUtils.bindings.h")]
	public static class Utils
	{
		// Token: 0x06002B67 RID: 11111
		[FreeFunction("DiagnosticsUtils_Bindings::ForceCrash", IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceCrash(ForcedCrashCategory crashCategory);

		// Token: 0x06002B68 RID: 11112 RVA: 0x00052FE0 File Offset: 0x000511E0
		[FreeFunction("DiagnosticsUtils_Bindings::NativeAssert", IsThreadSafe = true)]
		public unsafe static void NativeAssert(string message)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(message, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = message.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Utils.NativeAssert_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x00053034 File Offset: 0x00051234
		[FreeFunction("DiagnosticsUtils_Bindings::NativeError", IsThreadSafe = true)]
		public unsafe static void NativeError(string message)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(message, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = message.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Utils.NativeError_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x00053088 File Offset: 0x00051288
		[FreeFunction("DiagnosticsUtils_Bindings::NativeWarning", IsThreadSafe = true)]
		public unsafe static void NativeWarning(string message)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(message, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = message.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Utils.NativeWarning_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002B6B RID: 11115
		[FreeFunction("ValidateHeap")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ValidateHeap();

		// Token: 0x06002B6C RID: 11116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NativeAssert_Injected(ref ManagedSpanWrapper message);

		// Token: 0x06002B6D RID: 11117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NativeError_Injected(ref ManagedSpanWrapper message);

		// Token: 0x06002B6E RID: 11118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NativeWarning_Injected(ref ManagedSpanWrapper message);
	}
}
