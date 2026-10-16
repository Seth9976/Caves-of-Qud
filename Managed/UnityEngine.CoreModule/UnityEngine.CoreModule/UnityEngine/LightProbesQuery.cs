using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000161 RID: 353
	[NativeContainer]
	[NativeHeader("Runtime/Camera/RenderLoops/LightProbeContext.h")]
	[StaticAccessor("LightProbeContextWrapper", StaticAccessorType.DoubleColon)]
	public struct LightProbesQuery : IDisposable
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00015344 File Offset: 0x00013544
		public LightProbesQuery(Allocator allocator)
		{
			this.m_LightProbeContextWrapper = LightProbesQuery.Create();
			this.m_AllocatorLabel = allocator;
			UnsafeUtility.LeakRecord(this.m_LightProbeContextWrapper, LeakCategory.LightProbesQuery, 0);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00015368 File Offset: 0x00013568
		public void Dispose()
		{
			bool flag = this.m_LightProbeContextWrapper == IntPtr.Zero;
			if (flag)
			{
				throw new ObjectDisposedException("The LightProbesQuery is already disposed.");
			}
			bool flag2 = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag2)
			{
				throw new InvalidOperationException("The LightProbesQuery can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			if (flag3)
			{
				UnsafeUtility.LeakErase(this.m_LightProbeContextWrapper, LeakCategory.LightProbesQuery);
				LightProbesQuery.Destroy(this.m_LightProbeContextWrapper);
				this.m_AllocatorLabel = Allocator.Invalid;
			}
			this.m_LightProbeContextWrapper = IntPtr.Zero;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000153EC File Offset: 0x000135EC
		public JobHandle Dispose(JobHandle inputDeps)
		{
			bool flag = this.m_AllocatorLabel == Allocator.Invalid;
			if (flag)
			{
				throw new InvalidOperationException("The LightProbesQuery can not be Disposed because it was not allocated with a valid allocator.");
			}
			bool flag2 = this.m_LightProbeContextWrapper == IntPtr.Zero;
			if (flag2)
			{
				throw new InvalidOperationException("The LightProbesQuery is already disposed.");
			}
			bool flag3 = this.m_AllocatorLabel > Allocator.None;
			JobHandle jobHandle2;
			if (flag3)
			{
				JobHandle jobHandle = new LightProbesQuery.LightProbesQueryDisposeJob
				{
					Data = new LightProbesQuery.LightProbesQueryDispose
					{
						m_LightProbeContextWrapper = this.m_LightProbeContextWrapper
					}
				}.Schedule(inputDeps);
				this.m_AllocatorLabel = Allocator.Invalid;
				this.m_LightProbeContextWrapper = IntPtr.Zero;
				jobHandle2 = jobHandle;
			}
			else
			{
				this.m_LightProbeContextWrapper = IntPtr.Zero;
				jobHandle2 = inputDeps;
			}
			return jobHandle2;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0001549C File Offset: 0x0001369C
		public bool IsCreated
		{
			get
			{
				return this.m_LightProbeContextWrapper != IntPtr.Zero;
			}
		}

		// Token: 0x06000B7C RID: 2940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create();

		// Token: 0x06000B7D RID: 2941
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr lightProbeContextWrapper);

		// Token: 0x06000B7E RID: 2942 RVA: 0x000154BE File Offset: 0x000136BE
		public void CalculateInterpolatedLightAndOcclusionProbe(Vector3 position, ref int tetrahedronIndex, out SphericalHarmonicsL2 lightProbe, out Vector4 occlusionProbe)
		{
			LightProbesQuery.CalculateInterpolatedLightAndOcclusionProbe(this.m_LightProbeContextWrapper, position, ref tetrahedronIndex, out lightProbe, out occlusionProbe);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000154D4 File Offset: 0x000136D4
		public void CalculateInterpolatedLightAndOcclusionProbes(NativeArray<Vector3> positions, NativeArray<int> tetrahedronIndices, NativeArray<SphericalHarmonicsL2> lightProbes, NativeArray<Vector4> occlusionProbes)
		{
			bool flag = tetrahedronIndices.Length < positions.Length;
			if (flag)
			{
				throw new ArgumentException("tetrahedronIndices", "Argument tetrahedronIndices is null or has fewer elements than positions.");
			}
			bool flag2 = lightProbes.Length < positions.Length;
			if (flag2)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes is null or has fewer elements than positions.");
			}
			bool flag3 = occlusionProbes.Length < positions.Length;
			if (flag3)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes is null or has fewer elements than positions.");
			}
			LightProbesQuery.CalculateInterpolatedLightAndOcclusionProbes(this.m_LightProbeContextWrapper, (IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), (IntPtr)tetrahedronIndices.GetUnsafeReadOnlyPtr<int>(), (IntPtr)lightProbes.GetUnsafePtr<SphericalHarmonicsL2>(), (IntPtr)occlusionProbes.GetUnsafePtr<Vector4>(), positions.Length);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00015594 File Offset: 0x00013794
		[ThreadSafe]
		private static void CalculateInterpolatedLightAndOcclusionProbe(IntPtr lightProbeContextWrapper, Vector3 position, ref int tetrahedronIndex, out SphericalHarmonicsL2 lightProbe, out Vector4 occlusionProbe)
		{
			LightProbesQuery.CalculateInterpolatedLightAndOcclusionProbe_Injected(lightProbeContextWrapper, ref position, ref tetrahedronIndex, out lightProbe, out occlusionProbe);
		}

		// Token: 0x06000B81 RID: 2945
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateInterpolatedLightAndOcclusionProbes(IntPtr lightProbeContextWrapper, IntPtr positions, IntPtr tetrahedronIndices, IntPtr lightProbes, IntPtr occlusionProbes, int count);

		// Token: 0x06000B82 RID: 2946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateInterpolatedLightAndOcclusionProbe_Injected(IntPtr lightProbeContextWrapper, [In] ref Vector3 position, ref int tetrahedronIndex, out SphericalHarmonicsL2 lightProbe, out Vector4 occlusionProbe);

		// Token: 0x0400048B RID: 1163
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_LightProbeContextWrapper;

		// Token: 0x0400048C RID: 1164
		internal Allocator m_AllocatorLabel;

		// Token: 0x02000162 RID: 354
		[NativeContainer]
		internal struct LightProbesQueryDispose
		{
			// Token: 0x06000B83 RID: 2947 RVA: 0x000155AD File Offset: 0x000137AD
			public void Dispose()
			{
				UnsafeUtility.LeakErase(this.m_LightProbeContextWrapper, LeakCategory.LightProbesQuery);
				LightProbesQuery.Destroy(this.m_LightProbeContextWrapper);
			}

			// Token: 0x0400048D RID: 1165
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_LightProbeContextWrapper;
		}

		// Token: 0x02000163 RID: 355
		internal struct LightProbesQueryDisposeJob : IJob
		{
			// Token: 0x06000B84 RID: 2948 RVA: 0x000155C9 File Offset: 0x000137C9
			public void Execute()
			{
				this.Data.Dispose();
			}

			// Token: 0x0400048E RID: 1166
			internal LightProbesQuery.LightProbesQueryDispose Data;
		}
	}
}
