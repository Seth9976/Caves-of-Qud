using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200006B RID: 107
	public struct ParticleSystemJobData
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00008AF6 File Offset: 0x00006CF6
		public readonly int count { get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00008AFE File Offset: 0x00006CFE
		public readonly ParticleSystemNativeArray3 positions { get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00008B06 File Offset: 0x00006D06
		public readonly ParticleSystemNativeArray3 velocities { get; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00008B0E File Offset: 0x00006D0E
		public readonly ParticleSystemNativeArray3 axisOfRotations { get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00008B16 File Offset: 0x00006D16
		public readonly ParticleSystemNativeArray3 rotations { get; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00008B1E File Offset: 0x00006D1E
		public readonly ParticleSystemNativeArray3 rotationalSpeeds { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00008B26 File Offset: 0x00006D26
		public readonly ParticleSystemNativeArray3 sizes { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00008B2E File Offset: 0x00006D2E
		public readonly NativeArray<Color32> startColors { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00008B36 File Offset: 0x00006D36
		public readonly NativeArray<float> aliveTimePercent { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00008B3E File Offset: 0x00006D3E
		public readonly NativeArray<float> inverseStartLifetimes { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00008B46 File Offset: 0x00006D46
		public readonly NativeArray<uint> randomSeeds { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00008B4E File Offset: 0x00006D4E
		public readonly ParticleSystemNativeArray4 customData1 { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00008B56 File Offset: 0x00006D56
		public readonly ParticleSystemNativeArray4 customData2 { get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00008B5E File Offset: 0x00006D5E
		public readonly NativeArray<int> meshIndices { get; }

		// Token: 0x060006E3 RID: 1763 RVA: 0x00008B68 File Offset: 0x00006D68
		internal ParticleSystemJobData(ref NativeParticleData nativeData)
		{
			this = default(ParticleSystemJobData);
			this.count = nativeData.count;
			this.positions = this.CreateNativeArray3(ref nativeData.positions, this.count);
			this.velocities = this.CreateNativeArray3(ref nativeData.velocities, this.count);
			this.axisOfRotations = this.CreateNativeArray3(ref nativeData.axisOfRotations, this.count);
			this.rotations = this.CreateNativeArray3(ref nativeData.rotations, this.count);
			this.rotationalSpeeds = this.CreateNativeArray3(ref nativeData.rotationalSpeeds, this.count);
			this.sizes = this.CreateNativeArray3(ref nativeData.sizes, this.count);
			this.startColors = this.CreateNativeArray<Color32>(nativeData.startColors, this.count);
			this.aliveTimePercent = this.CreateNativeArray<float>(nativeData.aliveTimePercent, this.count);
			this.inverseStartLifetimes = this.CreateNativeArray<float>(nativeData.inverseStartLifetimes, this.count);
			this.randomSeeds = this.CreateNativeArray<uint>(nativeData.randomSeeds, this.count);
			this.customData1 = this.CreateNativeArray4(ref nativeData.customData1, this.count);
			this.customData2 = this.CreateNativeArray4(ref nativeData.customData2, this.count);
			this.meshIndices = this.CreateNativeArray<int>(nativeData.meshIndices, this.count);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00008CC4 File Offset: 0x00006EC4
		internal unsafe NativeArray<T> CreateNativeArray<T>(void* src, int count) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(src, count, Allocator.Invalid);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00008CE0 File Offset: 0x00006EE0
		internal unsafe ParticleSystemNativeArray3 CreateNativeArray3(ref NativeParticleData.Array3 ptrs, int count)
		{
			return new ParticleSystemNativeArray3
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count)
			};
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00008D38 File Offset: 0x00006F38
		internal unsafe ParticleSystemNativeArray4 CreateNativeArray4(ref NativeParticleData.Array4 ptrs, int count)
		{
			return new ParticleSystemNativeArray4
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count),
				w = this.CreateNativeArray<float>((void*)ptrs.w, count)
			};
		}
	}
}
