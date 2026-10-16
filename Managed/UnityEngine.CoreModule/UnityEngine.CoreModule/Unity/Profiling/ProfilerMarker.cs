using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000066 RID: 102
	[IgnoredByDeepProfiler]
	[UsedByNativeCode]
	public struct ProfilerMarker
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00003EC4 File Offset: 0x000020C4
		public IntPtr Handle
		{
			get
			{
				return this.m_Ptr;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00003ECC File Offset: 0x000020CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00003EDE File Offset: 0x000020DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarker(char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00003EF1 File Offset: 0x000020F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, MarkerFlags.Default, 0);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00003F08 File Offset: 0x00002108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, MarkerFlags.Default, 0);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00003F20 File Offset: 0x00002120
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name, MarkerFlags flags)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, flags, 0);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00003F37 File Offset: 0x00002137
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen, MarkerFlags flags)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, flags, 0);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00003F50 File Offset: 0x00002150
		[Pure]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00003F5F File Offset: 0x0000215F
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin(Object contextUnityObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, contextUnityObject);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003F6F File Offset: 0x0000216F
		[Pure]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003F7E File Offset: 0x0000217E
		[Conditional("ENABLE_PROFILER")]
		internal void GetName(ref string name)
		{
			name = ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00003F90 File Offset: 0x00002190
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}

		// Token: 0x04000170 RID: 368
		[NativeDisableUnsafePtrRestriction]
		[NonSerialized]
		internal readonly IntPtr m_Ptr;

		// Token: 0x02000067 RID: 103
		[IgnoredByDeepProfiler]
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			// Token: 0x060001B3 RID: 435 RVA: 0x00003FB0 File Offset: 0x000021B0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.BeginSample(markerPtr);
				}
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x00003FE0 File Offset: 0x000021E0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.EndSample(this.m_Ptr);
				}
			}

			// Token: 0x04000171 RID: 369
			[NativeDisableUnsafePtrRestriction]
			internal readonly IntPtr m_Ptr;
		}
	}
}
