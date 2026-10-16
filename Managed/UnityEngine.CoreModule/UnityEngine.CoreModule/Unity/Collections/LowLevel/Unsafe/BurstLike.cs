using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Burst.LowLevel;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000B0 RID: 176
	[NativeHeader("Runtime/Export/BurstLike/BurstLike.bindings.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
	[StaticAccessor("BurstLike", StaticAccessorType.DoubleColon)]
	internal static class BurstLike
	{
		// Token: 0x060003BF RID: 959
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int NativeFunctionCall_Int_IntPtr_IntPtr(IntPtr function, IntPtr p0, IntPtr p1, out int error);

		// Token: 0x020000B1 RID: 177
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal readonly struct SharedStatic<[IsUnmanaged] T> where T : struct, ValueType
		{
			// Token: 0x060003C0 RID: 960 RVA: 0x00007B41 File Offset: 0x00005D41
			private unsafe SharedStatic(void* buffer)
			{
				this._buffer = buffer;
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060003C1 RID: 961 RVA: 0x00007B4A File Offset: 0x00005D4A
			public ref T Data
			{
				get
				{
					return UnsafeUtility.AsRef<T>(this._buffer);
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x00007B57 File Offset: 0x00005D57
			public unsafe void* UnsafeDataPointer
			{
				get
				{
					return this._buffer;
				}
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x00007B5F File Offset: 0x00005D5F
			public static BurstLike.SharedStatic<T> GetOrCreate<TContext>(uint alignment = 0U)
			{
				return new BurstLike.SharedStatic<T>(BurstLike.SharedStatic.GetOrCreateSharedStaticInternal(BurstRuntime.GetHashCode64<TContext>(), 0L, (uint)UnsafeUtility.SizeOf<T>(), alignment));
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x00007B78 File Offset: 0x00005D78
			public static BurstLike.SharedStatic<T> GetOrCreate<TContext, TSubContext>(uint alignment = 0U)
			{
				return new BurstLike.SharedStatic<T>(BurstLike.SharedStatic.GetOrCreateSharedStaticInternal(BurstRuntime.GetHashCode64<TContext>(), BurstRuntime.GetHashCode64<TSubContext>(), (uint)UnsafeUtility.SizeOf<T>(), alignment));
			}

			// Token: 0x0400028A RID: 650
			private unsafe readonly void* _buffer;
		}

		// Token: 0x020000B2 RID: 178
		internal static class SharedStatic
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x00007B94 File Offset: 0x00005D94
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private static void CheckSizeOf(uint sizeOf)
			{
				bool flag = sizeOf == 0U;
				if (flag)
				{
					throw new ArgumentException("sizeOf must be > 0", "sizeOf");
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00007BBC File Offset: 0x00005DBC
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private unsafe static void CheckResult(void* result)
			{
				bool flag = result == null;
				if (flag)
				{
					throw new InvalidOperationException("Unable to create a SharedStatic for this key. This is most likely due to the size of the struct inside of the SharedStatic having changed or the same key being reused for differently sized values. To fix this the editor needs to be restarted.");
				}
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x00007BE0 File Offset: 0x00005DE0
			[RequiredMember]
			public unsafe static void* GetOrCreateSharedStaticInternal(long getHashCode64, long getSubHashCode64, uint sizeOf, uint alignment)
			{
				Hash128 hash = new Hash128((ulong)getHashCode64, (ulong)getSubHashCode64);
				return BurstCompilerService.GetOrCreateSharedMemory(ref hash, sizeOf, (alignment == 0U) ? 4U : alignment);
			}
		}
	}
}
