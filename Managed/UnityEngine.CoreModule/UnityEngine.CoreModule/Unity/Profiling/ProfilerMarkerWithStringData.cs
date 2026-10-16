using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;

namespace Unity.Profiling
{
	// Token: 0x0200006B RID: 107
	internal struct ProfilerMarkerWithStringData
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00004010 File Offset: 0x00002210
		public static ProfilerMarkerWithStringData Create(string name, string parameterName)
		{
			IntPtr intPtr = ProfilerUnsafeUtility.CreateMarker(name, 16, MarkerFlags.Default, 1);
			ProfilerUnsafeUtility.SetMarkerMetadata(intPtr, 0, parameterName, 9, 0);
			return new ProfilerMarkerWithStringData
			{
				_marker = intPtr
			};
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000404C File Offset: 0x0000224C
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarkerWithStringData.AutoScope Auto(bool enabled, Func<string> parameterValue)
		{
			ProfilerMarkerWithStringData.AutoScope autoScope;
			if (enabled)
			{
				autoScope = this.Auto(parameterValue());
			}
			else
			{
				autoScope = new ProfilerMarkerWithStringData.AutoScope(IntPtr.Zero);
			}
			return autoScope;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004080 File Offset: 0x00002280
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarkerWithStringData.AutoScope Auto(string value)
		{
			bool flag = value == null;
			if (flag)
			{
				throw new ArgumentNullException("value");
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				ProfilerMarkerData profilerMarkerData = new ProfilerMarkerData
				{
					Type = 9,
					Size = (uint)(value.Length * 2 + 2)
				};
				profilerMarkerData.Ptr = (void*)ptr;
				ProfilerUnsafeUtility.BeginSampleWithMetadata(this._marker, 1, (void*)(&profilerMarkerData));
			}
			return new ProfilerMarkerWithStringData.AutoScope(this._marker);
		}

		// Token: 0x04000182 RID: 386
		private const MethodImplOptions AggressiveInlining = MethodImplOptions.AggressiveInlining;

		// Token: 0x04000183 RID: 387
		private IntPtr _marker;

		// Token: 0x0200006C RID: 108
		public struct AutoScope : IDisposable
		{
			// Token: 0x060001B8 RID: 440 RVA: 0x00004106 File Offset: 0x00002306
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(IntPtr marker)
			{
				this._marker = marker;
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x00004110 File Offset: 0x00002310
			[Pure]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				bool flag = this._marker != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.EndSample(this._marker);
				}
			}

			// Token: 0x04000184 RID: 388
			private IntPtr _marker;
		}
	}
}
