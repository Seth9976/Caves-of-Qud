using System;
using System.Diagnostics;
using Unity.Profiling;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x02000301 RID: 769
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/Marker.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	public sealed class CustomSampler : Sampler
	{
		// Token: 0x0600264C RID: 9804 RVA: 0x00049FA9 File Offset: 0x000481A9
		internal CustomSampler()
		{
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x00049FB3 File Offset: 0x000481B3
		private CustomSampler(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00049FC0 File Offset: 0x000481C0
		public static CustomSampler Create(string name, bool collectGpuData = false)
		{
			IntPtr intPtr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.AvailabilityNonDevelopment | (collectGpuData ? MarkerFlags.SampleGPU : MarkerFlags.Default), 0);
			bool flag = intPtr == IntPtr.Zero;
			CustomSampler customSampler;
			if (flag)
			{
				customSampler = CustomSampler.s_InvalidCustomSampler;
			}
			else
			{
				customSampler = new CustomSampler(intPtr);
			}
			return customSampler;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0004A005 File Offset: 0x00048205
		[Conditional("ENABLE_PROFILER")]
		[IgnoredByDeepProfiler]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0004A014 File Offset: 0x00048214
		[Conditional("ENABLE_PROFILER")]
		[IgnoredByDeepProfiler]
		public void Begin(Object targetObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, targetObject);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0004A024 File Offset: 0x00048224
		[Conditional("ENABLE_PROFILER")]
		[IgnoredByDeepProfiler]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x04000ABE RID: 2750
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();

		// Token: 0x02000302 RID: 770
		internal static class BindingsMarshaller
		{
			// Token: 0x06002653 RID: 9811 RVA: 0x0004A03F File Offset: 0x0004823F
			public static IntPtr ConvertToNative(CustomSampler customSampler)
			{
				return customSampler.m_Ptr;
			}
		}
	}
}
